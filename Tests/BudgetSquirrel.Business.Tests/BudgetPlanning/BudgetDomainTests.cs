using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils;
using BudgetSquirrel.TestUtils.Budgeting;
using Xunit;

namespace BudgetSquirrel.Business.Tests.BudgetPlanning
{
  public class BudgetDomainTests : IDisposable
    {
        private Faker _faker = new Faker();
        private BuilderFactoryFixture _builderFactoryFixture;

        public BudgetDomainTests()
        {
            _builderFactoryFixture = new BuilderFactoryFixture();
        }

        public void Dispose()
        {
            _builderFactoryFixture.Dispose();
        }

        [Fact]
        public void Test_IsPercentBasedBudget_IsTrue_WhenPercentAmountSet()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder
                                .SetParentBudget(_builderFactoryFixture.BudgetBuilder.Build())
                                .SetPercentAmount(0.25)
                                .Build();
            Assert.True(subject.IsPercentBasedBudget);
        }

        [Fact]
        public void Test_IsPercentBasedBudget_IsFalse_WhenFixedAmountSet()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder
                                .SetFixedAmount(10)
                                .Build();
            Assert.False(subject.IsPercentBasedBudget);
        }

        [Fact]
        public void Test_IsPercentBasedBudget_IsTrue_WhenChangedFromFixedAmount()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder
                                .SetParentBudget(
                                    _builderFactoryFixture.BudgetBuilder.SetFixedAmount(10).Build())
                                .Build();
            subject.SetPercentAmount(0.3);

            Assert.True(subject.IsPercentBasedBudget);
        }

        [Fact]
        public void Test_IsPercentBasedBudget_IsFalse_WhenChangedFromPercentAmount()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder
                                .SetParentBudget(_builderFactoryFixture.BudgetBuilder.Build())
                                .SetPercentAmount(0.25)
                                .Build();
            subject.SetFixedAmount(3);

            Assert.False(subject.IsPercentBasedBudget);
        }

        [Fact]
        public void Test_ErrorThrown_WhenSetFixedAmountCalled_WithNegative()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder.Build();
            Assert.Throws<InvalidOperationException>(() => subject.SetFixedAmount(-3));
        }

        [Fact]
        public void Test_ErrorThrown_WhenSetPercentAmountCalled_WithNegative()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder.Build();
            Assert.Throws<InvalidOperationException>(() => subject.SetPercentAmount(-0.3));
        }

        [Fact]
        public void Test_ErrorThrown_WhenSetPercentAmountCalled_WithGreaterThan100Percent()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder.Build();
            Assert.Throws<InvalidOperationException>(() => subject.SetPercentAmount(1.5));
        }

        [Fact]
        public void Test_SetAmountDefaultsTo0_WhenCreatingBudget()
        {
            IBudgetDurationBuilder durationBuilder = _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>().GetBuilder<MonthlyBookEndedDuration>();
            BudgetDurationBase randomDuration = durationBuilder.Build();
            Fund fund = _builderFactoryFixture.FundBuilder.Build();
            DateTime now = DateTime.Now;

            Budget subject = new Budget(
                fund,
                new BudgetPeriod(now, fund.Duration.GetEndDateFromStartDate(now))
            );

            decimal expectedDefaultSetAmount = 0;

            Assert.Equal(expectedDefaultSetAmount, subject.SetAmount);
        }

        [Theory]
        [InlineData(12, 46, 58)]
        [InlineData(11, -45, -34)]
        public void Test_FundBalanceCorrect_WhenAddToFundCalled(decimal startBalance, decimal add, decimal expected)
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder
                                .SetFund(fundBuilder =>
                                    fundBuilder.SetFundBalance(startBalance))
                                .Build();

            subject.Fund.AddToFund(add);

            Assert.Equal(expected, subject.Fund.FundBalance);
        }
    }
}