using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
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
                                .SetFixedAmount(10)
                                .Build();
            subject.SetPercentAmount(0.3);

            Assert.True(subject.IsPercentBasedBudget);
        }

        [Fact]
        public void Test_IsPercentBasedBudget_IsFalse_WhenChangedFromPercentAmount()
        {
            Budget subject = _builderFactoryFixture.BudgetBuilder
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

            Budget subject = new Budget(
                _faker.Lorem.Word(),
                _faker.Random.Decimal(),
                randomDuration,
                _faker.Date.Past()
            );

            decimal expectedDefaultSetAmount = 0;

            Assert.Equal(expectedDefaultSetAmount, subject.SetAmount);
        }

        [Fact]
        public void Test_SetAmountDefaultsTo0_WhenCreatingBudget_WithId()
        {
            IBudgetDurationBuilder durationBuilder = _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>().GetBuilder<MonthlyBookEndedDuration>();
            BudgetDurationBase randomDuration = durationBuilder.Build();

            Budget subject = new Budget(
                Guid.NewGuid(),
                _faker.Lorem.Word(),
                _faker.Random.Decimal(),
                randomDuration,
                _faker.Date.Past()
            );

            decimal expectedDefaultSetAmount = 0;

            Assert.Equal(expectedDefaultSetAmount, subject.SetAmount);
        }
    }
}