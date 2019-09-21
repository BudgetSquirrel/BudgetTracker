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

        /// <summary>
        /// <p>
        /// Seeds the database with budgets.
        /// </p>
        /// <p>
        /// The root has 5 sub budgets. The sub budget with $340 also has 2 sub
        /// budgets of it's own.
        /// </p>
        /// </summary>
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


            await userRepository.Register(owner);
            await budgetRepository.CreateBudget(root);
            root.SubBudgets = await CreateSubBudgets(root, owner, budgetRepository);
            return (root);
        }

        private async Task<List<Budget>> CreateSubBudgets(Budget parent, User owner, IBudgetRepository budgetRepository)
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
            foreach (Budget b in subBudgets) await budgetRepository.CreateBudget(b);
            subBudgets[0].SubBudgets = await CreateDeepSubBudget(subBudgets[0], owner, budgetRepository);
            return subBudgets;
        }

        private async Task<List<Budget>> CreateDeepSubBudget(Budget parent, User owner, IBudgetRepository budgetRepository)
        {
            List<Budget> subBudgets = new List<Budget>()
            {
                StartSubBudgetBuild(parent, owner).SetFixedAmount(167).SetPercentAmount(null).Build(),
                StartSubBudgetBuild(parent, owner).SetFixedAmount(parent.SetAmount-167).SetPercentAmount(null).Build()
            };
            foreach (Budget b in subBudgets) await budgetRepository.CreateBudget(b);
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
