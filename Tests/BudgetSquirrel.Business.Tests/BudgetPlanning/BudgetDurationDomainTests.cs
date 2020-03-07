using System;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils.Budgeting;
using Xunit;

namespace BudgetSquirrel.Business.Tests.BudgetPlanning
{
    public class BudgetDurationDomainTests : IDisposable
    {
        private BuilderFactoryFixture _builderFactoryFixture;

        public BudgetDurationDomainTests()
        {
            _builderFactoryFixture = new BuilderFactoryFixture();
        }

        [Theory]
        [InlineData(2020, 2, 2, 24, 2020, 2, 26)]
        [InlineData(2020, 3, 7, 24, 2020, 4, 1)]
        [InlineData(2020, 2, 2, 24, 2020, 2, 26)]
        [InlineData(2020, 2, 2, 24, 2020, 2, 26)]
        public void Test_DaySpanDurationGetEndDate_ReturnsCorrect(int startYear, int startMonth, int startDay, int numberDays, int expectedYear, int expectedMonth, int expectedDay)
        {
            BudgetDurationBuilderProvider builderProvider = _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>();
            DaySpanDuration subject = (DaySpanDuration) ((DaySpanDurationBuilder) builderProvider.GetBuilder<DaySpanDuration>())
                                        .SetNumberDays(numberDays)
                                        .Build();

            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime expected = new DateTime(expectedYear, expectedMonth, expectedDay);
            DateTime actual = subject.GetEndDateFromStartDate(startDate);

            Assert.Equal(actual, expected);
        }

        public void Dispose()
        {
            _builderFactoryFixture.Dispose();
        }
    }
}