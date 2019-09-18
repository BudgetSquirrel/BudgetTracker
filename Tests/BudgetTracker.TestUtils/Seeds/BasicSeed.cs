using BudgetTracker.Business.Auth;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using BudgetTracker.Business.Transactions;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.TestUtils.Auth;
using BudgetTracker.TestUtils.Budgeting;
using BudgetTracker.TestUtils.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.TestUtils.Seeds
{
    public class BasicSeed
    {
        private UserFactory _userFactory;
        private BudgetBuilderFactory<Budget> _budgetBuilderFactory;

        private decimal ROOT_AMOUNT = 3500;

        public BasicSeed(BudgetBuilderFactory<Budget> budgetBuilderFactory,
            UserFactory userFactory)
        {
            _budgetBuilderFactory = budgetBuilderFactory;
            _userFactory = userFactory;
        }

        public async Task<Budget> Seed(IUserRepository userRepository,
            IBudgetRepository budgetRepository)
        {
            User owner = _userFactory.NewUser();
            Budget root = _budgetBuilderFactory.GetBuilder()
                                                .SetFixedAmount(ROOT_AMOUNT)
                                                .SetPercentAmount(null)
                                                .SetOwner(owner)
                                                .SetDurationNumberDays(30)
                                                .Build();

            root.SubBudgets = CreateSubBudgets(root, owner);

            await userRepository.Register(owner);
            await budgetRepository.CreateBudget(root);
            foreach (Budget b in root.SubBudgets) await budgetRepository.CreateBudget(b);
            return (root);
        }

        private List<Budget> CreateSubBudgets(Budget parent, User owner)
        {
            List<Budget> subBudgets = new List<Budget>()
            {
                StartSubBudgetBuild(parent, owner).SetFixedAmount(340).SetPercentAmount(null).Build(),
                StartSubBudgetBuild(parent, owner).SetFixedAmount(null).SetPercentAmount(0.2).Build(),
                StartSubBudgetBuild(parent, owner).SetFixedAmount(120).SetPercentAmount(null).Build(),
                StartSubBudgetBuild(parent, owner).SetFixedAmount(30).SetPercentAmount(null).Build(),
                StartSubBudgetBuild(parent, owner).SetFixedAmount(75).SetPercentAmount(null).Build()
            };
            decimal subBudgetsTotal = 340 + 120 + 30 + 75 + (ROOT_AMOUNT * (decimal)0.2);
            decimal remainingMoney = ROOT_AMOUNT - subBudgetsTotal;
            subBudgets.Add(StartSubBudgetBuild(parent, owner)
                                            .SetName("Main Savings")
                                            .SetFixedAmount(remainingMoney)
                                            .SetPercentAmount(null)
                                            .Build());
            return subBudgets;
        }

        private IBudgetBuilder<Budget> StartSubBudgetBuild(Budget parent, User owner)
        {
            return _budgetBuilderFactory.GetBuilder()
                                        .SetParentBudget(parent)
                                        .SetOwner(owner);
        }

        private BudgetPeriod SeedPeriodForRootBudget(Budget rootBudget)
        {
            BudgetPeriod period = new BudgetPeriod()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(((MonthlyDaySpanDuration) rootBudget.Duration).NumberDays),
                RootBudget = rootBudget,
                RootBudgetId = rootBudget.Id
            };
            return period;
        }
    }
}
