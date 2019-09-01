using BudgetTracker.Business.Auth;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Business.Tests;
using BudgetTracker.TestUtils.Auth;
using BudgetTracker.TestUtils.Budgeting;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BudgetTracker.Business.Tests.UnitTests
{
    public class BudgetCreateTests : BaseUnitTest
    {
        private BudgetBuilderFactory<Budget> _budgetBuilderFactory;
        private UserFactory _userFactory;

        public BudgetCreateTests()
            : base()
        {
            _budgetBuilderFactory = GetService<BudgetBuilderFactory<Budget>>();
            _userFactory = GetService<UserFactory>();
        }

        [Fact]
        public async Task Test_BudgetCreated_When_IsRootBudget()
        {
            Mock<IBudgetRepository> mockBudgetRepository = new Mock<IBudgetRepository>();
            User owner = _userFactory.NewUser();

            Budget budget = _budgetBuilderFactory.GetBuilder()
                                .SetPercentAmount(null)
                                .SetFixedAmount(47)
                                .SetOwner(owner)
                                .Build();

            mockBudgetRepository.Setup(repo => repo.CreateBudget(It.IsAny<Budget>()))
                                .Returns(Task.FromResult(budget));

            Budget createdBudget = await BudgetCreation.CreateBudgetForUser(budget, owner, mockBudgetRepository.Object);

            mockBudgetRepository.Verify(repo => repo.CreateBudget(budget));
            Assert.Equal(createdBudget.Name, budget.Name);
        }

        // (ChildPercentAmount, ChildSetAmount, ParentSetAmount, AmountExpected)
        [Theory]
        [InlineData(.34,    null,   59,     20.06)]
        [InlineData(.64,    null,   58,     37.12)]
        [InlineData(null,   59,     64,     59)]
        [InlineData(null,   99,     100,    99)]
        public async Task Test_BudgetSetAmountSet_When_Created_And_IsSubBudget(double? percentAmount, double? setAmountTmp, decimal parentSetAmount, decimal expectedSetAmount)
        {
            decimal? setAmount = (decimal?) setAmountTmp;
            Mock<IBudgetRepository> mockBudgetRepository = new Mock<IBudgetRepository>();
            User owner = _userFactory.NewUser();

            Budget parent = _budgetBuilderFactory.GetBuilder()
                                .SetFixedAmount(parentSetAmount)
                                .SetOwner(owner)
                                .Build();

            Budget child = _budgetBuilderFactory.GetBuilder()
                                    .SetPercentAmount(percentAmount)
                                    .SetFixedAmount(setAmount)
                                    .SetParentBudget(parent)
                                    .Build();

            mockBudgetRepository.Setup(repo => repo.CreateBudget(It.IsAny<Budget>()))
                                .Returns(Task.FromResult(child));
            mockBudgetRepository.Setup(repo => repo.GetBudget(parent.Id))
                                .Returns(Task.FromResult(parent));

            Budget created = await BudgetCreation.CreateBudgetForUser(child, owner, mockBudgetRepository.Object);
            Assert.Equal(expectedSetAmount, created.SetAmount);
        }
    }
}
