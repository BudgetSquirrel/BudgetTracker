using System;
using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Tracking;
using BudgetSquirrel.TestUtils.Budgeting;
using Xunit;

namespace BudgetSquirrel.Business.Tests.Tracking
{
    public class UpdateBudgetPeriodsCommandTests : IDisposable
    {
        private BuilderFactoryFixture _builderFactoryFixture;

        public UpdateBudgetPeriodsCommandTests()
        {
            _builderFactoryFixture = new BuilderFactoryFixture();
        }

        [Fact]
        public void Test_CreatedBudget_CorrectEndDate_WhenNextPeriodIsDue()
        {
            int durationLength = 29;
            BudgetPeriod lastPeriod = new BudgetPeriod(new DateTime(2020, 3, 1), new DateTime(2020, 3, 30));
            DateTime dateToTest = lastPeriod.EndDate.AddDays(6);
            DateTime expectedEndDate = lastPeriod.EndDate.AddDays(durationLength + 1);

            BudgetDurationBase duration = ((DaySpanDurationBuilder) _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>()
                                            .GetBuilder<DaySpanDuration>())
                                            .SetNumberDays(durationLength)
                                            .Build();

            UpdateBudgetPeriodsCommand subject = new UpdateBudgetPeriodsCommand();

            IEnumerable<BudgetPeriod> created = subject.Run(lastPeriod, duration, dateToTest);

            Assert.Equal(expectedEndDate, created.Last().EndDate);
        }

        [Fact]
        public void Test_UpdateReturns3Budgets_WhenCalledForToday_AndLastPeriodIs3PeriodsBack()
        {
            int durationLength = 29;
            int expectedNumDurationsCreated = 3;
            BudgetPeriod lastPeriod = new BudgetPeriod(new DateTime(2020, 3, 1), new DateTime(2020, 3, 30));
            DateTime dateToTest = lastPeriod.EndDate.AddDays(durationLength * expectedNumDurationsCreated);

            BudgetDurationBase duration = ((DaySpanDurationBuilder) _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>()
                                            .GetBuilder<DaySpanDuration>())
                                            .SetNumberDays(durationLength)
                                            .Build();

            UpdateBudgetPeriodsCommand subject = new UpdateBudgetPeriodsCommand();

            IEnumerable<BudgetPeriod> created = subject.Run(lastPeriod, duration, dateToTest);

            Assert.Equal(3, created.Count());
        }

        public void Dispose()
        {
            _builderFactoryFixture.Dispose();
        }
    }
}