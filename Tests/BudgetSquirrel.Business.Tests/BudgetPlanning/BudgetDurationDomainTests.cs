using System;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils;
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
        [InlineData(2020, 2, 2, 24, 2020, 2, 25)]
        [InlineData(2020, 3, 9, 24, 2020, 4, 1)]
        [InlineData(2020, 2, 20, 24, 2020, 3, 14)]
        [InlineData(2019, 12, 20, 24, 2020, 1, 12)]
        public void Test_DaySpanDurationGetEndDate_ReturnsCorrect(int startYear, int startMonth, int startDay, int numberDays, int expectedYear, int expectedMonth, int expectedDay)
        {
            BudgetDurationBuilderProvider builderProvider = _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>();
            DaySpanDuration subject = (DaySpanDuration) ((DaySpanDurationBuilder) builderProvider.GetBuilder<DaySpanDuration>())
                                        .SetNumberDays(numberDays)
                                        .Build();

            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime expected = new DateTime(expectedYear, expectedMonth, expectedDay);
            DateTime actual = subject.GetEndDateFromStartDate(startDate);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_CreateDaySpan_ThrowsError_WhenZeroNumberDays()
        {
            Assert.Throws<InvalidOperationException>(() => new DaySpanDuration(0));
        }

        [Fact]
        public void Test_CreateDaySpan_ThrowsError_WhenNegativeDays()
        {
            Assert.Throws<InvalidOperationException>(() => new DaySpanDuration(-2));
        }

        [Theory]
        [InlineData(2020, 2, 2, 26, true, 2020, 2, 26)]
        [InlineData(2020, 2, 2, 30, true, 2020, 3, 1)]
        [InlineData(2020, 2, 2, 30, false, 2020, 2, 29)]
        [InlineData(2019, 12, 2, 30, false, 2019, 12, 30)]
        public void Test_BookendedDurationGetEndDate_ReturnsCorrect(int startYear, int startMonth, int startDay, int endDate, bool rolover, int expectedYear, int expectedMonth, int expectedDay)
        {
            BudgetDurationBuilderProvider builderProvider = _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>();
            MonthlyBookEndedDuration subject = (MonthlyBookEndedDuration) ((MonthlyBookEndedDurationBuilder) builderProvider.GetBuilder<MonthlyBookEndedDuration>())
                                        .SetDurationEndDayOfMonth(endDate)
                                        .SetDurationRolloverEndDateOnSmallMonths(rolover)
                                        .Build();

            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime expected = new DateTime(expectedYear, expectedMonth, expectedDay);
            DateTime actual = subject.GetEndDateFromStartDate(startDate);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_CreateBookEnded_ThrowsError_WhenEndsOn0th()
        {
            Assert.Throws<InvalidOperationException>(() => new MonthlyBookEndedDuration(0, true));
        }

        [Fact]
        public void Test_CreateBookEnded_ThrowsError_WhenEndsOnNegative()
        {
            Assert.Throws<InvalidOperationException>(() => new MonthlyBookEndedDuration(-2, true));
        }

        [Fact]
        public void Test_CreateBookEnded_ThrowsError_WhenEndsOn32nd()
        {
            Assert.Throws<InvalidOperationException>(() => new MonthlyBookEndedDuration(32, true));
        }

        public void Dispose()
        {
            _builderFactoryFixture.Dispose();
        }
    }
}