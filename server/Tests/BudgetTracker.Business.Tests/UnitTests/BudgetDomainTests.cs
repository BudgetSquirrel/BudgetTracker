using BudgetTracker.Business.Budgeting;
using BudgetTracker.TestUtils.Budgeting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BudgetTracker.Business.Tests.UnitTests
{
    public class BudgetDomainTests : BaseUnitTest
    {
        private BudgetBuilderFactory<Budget> _budgetBuilderFactory;

        public BudgetDomainTests()
            : base()
        {
            _budgetBuilderFactory = GetService<BudgetBuilderFactory<Budget>>();
        }

        // (ChildPercentAmount, ChildSetAmount, ParentSetAmount, AmountExpected)
        [Theory]
        [InlineData(.10,    null,   100,    10)]
        [InlineData(1.00,   null,   100,    100)]
        [InlineData(.50,    null,   100,    50)]
        [InlineData(0,      null,   86,     0)]
        [InlineData(.26,    null,   97,     25.22)]
        [InlineData(.64,    null,   58,     37.12)]
        [InlineData(null,   59,     64,     59)]
        [InlineData(null,   99,     100,    99)]
        [InlineData(null,   100,    100,    100)]
        [InlineData(null,   0,      98,     0)]
        // For the following 3: Even if set budget is set, percent should be prioritized.
        [InlineData(.64,    10,     58,     37.12)]
        [InlineData(.26,    39,     97,     25.22)]
        [InlineData(0,      68,     86,     0)]
        public void Test_CalculateSetAmountReflectsParentsSetAmount_When_IsValidSubBudget_And_ParentIsFixedAmount(double? percentAmount, double? setAmountTmp, decimal parentSetAmount, decimal expectedSetAmount)
        {
            decimal? setAmount = (decimal?) setAmountTmp; // XUnit for some reason won't let me pass in decimal values.
            Budget parent = _budgetBuilderFactory.GetBuilder()
                                .SetFixedAmount(parentSetAmount)
                                .Build();

            Budget child = _budgetBuilderFactory.GetBuilder()
                                    .SetParentBudget(parent)
                                    .SetPercentAmount(percentAmount)
                                    .SetFixedAmount(setAmount)
                                    .Build();

            Assert.Equal(expectedSetAmount, child.CalculateBudgetSetAmount());
        }

        [Theory]
        [InlineData(100, 99)]
        [InlineData(40, 37)]
        [InlineData(40, 0)]
        public void Test_CalculateSetAmountThrowsException_When_IsSubBudget_And_SetAmountMoreThanParent(decimal setAmount, decimal parentSetAmount)
        {
            Budget parent = _budgetBuilderFactory.GetBuilder()
                                .SetFixedAmount(parentSetAmount)
                                .SetPercentAmount(null)
                                .Build();

            Budget child = _budgetBuilderFactory.GetBuilder()
                                    .SetParentBudget(parent)
                                    .SetFixedAmount(setAmount)
                                    .SetPercentAmount(null)
                                    .Build();

            Assert.Throws<Exception>(() => child.CalculateBudgetSetAmount());
        }
    }
}
