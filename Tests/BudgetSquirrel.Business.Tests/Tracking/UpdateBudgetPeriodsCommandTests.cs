// using System;
// using System.Collections.Generic;
// using System.Linq;
// using BudgetSquirrel.Business.BudgetPlanning;
// using BudgetSquirrel.Business.Tracking;
// using BudgetSquirrel.TestUtils.Budgeting;
// using Xunit;

// namespace BudgetSquirrel.Business.Tests.Tracking
// {
//     public class UpdateBudgetPeriodsCommandTests : IDisposable
//     {
//         private BuilderFactoryFixture _builderFactoryFixture;

//         public UpdateBudgetPeriodsCommandTests()
//         {
//             _builderFactoryFixture = new BuilderFactoryFixture();
//         }

//         [Fact]
//         public void Test_CreatedBudget_CorrectEndDate_WhenNextPeriodIsDue()
//         {
//             int durationLength = 29;
//             BudgetPeriod lastPeriod = new BudgetPeriod(new DateTime(2020, 3, 1), new DateTime(2020, 3, 30));
//             DateTime dateToTest = lastPeriod.EndDate.AddDays(6);
//             DateTime expectedEndDate = lastPeriod.EndDate.AddDays(durationLength);

//             BudgetDurationBase duration = ((DaySpanDurationBuilder) _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>()
//                                             .GetBuilder<DaySpanDuration>())
//                                             .SetNumberDays(durationLength)
//                                             .Build();

//             UpdateBudgetPeriodsCommand subject = new UpdateBudgetPeriodsCommand();

//             IEnumerable<BudgetPeriod> created = subject.Run(lastPeriod, duration, dateToTest);

//             Assert.Equal(expectedEndDate, created.Last().EndDate);
//         }

//         [Fact]
//         public void Test_CreatedBudget_ReturnsNothing_WhenNextPeriodIsNotDue()
//         {
//             int durationLength = 29;
//             BudgetPeriod lastPeriod = new BudgetPeriod(_builderFactoryFixture.BudgetBuilder.Build(), new DateTime(2020, 3, 1), new DateTime(2020, 3, 30));
//             DateTime dateToTest = lastPeriod.StartDate.AddDays(6);

//             BudgetDurationBase duration = ((DaySpanDurationBuilder) _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>()
//                                             .GetBuilder<DaySpanDuration>())
//                                             .SetNumberDays(durationLength)
//                                             .Build();

//             UpdateBudgetPeriodsCommand subject = new UpdateBudgetPeriodsCommand();

//             IEnumerable<BudgetPeriod> created = subject.Run(lastPeriod, duration, dateToTest);

//             Assert.Empty(created);
//         }

//         [Fact]
//         public void Test_UpdateReturns3Budgets_WhenCalledForToday_AndLastPeriodIs3PeriodsBack()
//         {
//             int durationLength = 29;
//             int expectedNumDurationsCreated = 3;
//             BudgetPeriod lastPeriod = new BudgetPeriod(_builderFactoryFixture.BudgetBuilder.Build(), new DateTime(2020, 3, 1), new DateTime(2020, 3, 30));
//             DateTime dateToTest = lastPeriod.EndDate.AddDays(durationLength * expectedNumDurationsCreated);

//             BudgetDurationBase duration = ((DaySpanDurationBuilder) _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>()
//                                             .GetBuilder<DaySpanDuration>())
//                                             .SetNumberDays(durationLength)
//                                             .Build();

//             UpdateBudgetPeriodsCommand subject = new UpdateBudgetPeriodsCommand();

//             IEnumerable<BudgetPeriod> created = subject.Run(lastPeriod, duration, dateToTest);

//             Assert.Equal(3, created.Count());
//         }

//         [Fact]
//         public void Test_2ndPeriodCorrectEndDate_When3PeirodsAreDue()
//         {
//             int durationLength = 4;
//             int expectedNumDurationsCreated = 3;
//             BudgetPeriod lastPeriod = new BudgetPeriod(_builderFactoryFixture.BudgetBuilder.Build(), new DateTime(2020, 3, 1), new DateTime(2020, 3, 30));
//             DateTime dateToTest = lastPeriod.EndDate.AddDays(durationLength * expectedNumDurationsCreated);
//             DateTime secondPeriodExpectedEndDate = lastPeriod.EndDate.AddDays(durationLength * 2);

//             BudgetDurationBase duration = ((DaySpanDurationBuilder) _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>()
//                                             .GetBuilder<DaySpanDuration>())
//                                             .SetNumberDays(durationLength)
//                                             .Build();

//             UpdateBudgetPeriodsCommand subject = new UpdateBudgetPeriodsCommand();

//             IEnumerable<BudgetPeriod> created = subject.Run(lastPeriod, duration, dateToTest);

//             BudgetPeriod secondPeriod = created.ToList()[1];

//             Assert.Equal(secondPeriodExpectedEndDate, secondPeriod.EndDate);
//         }

//         [Fact]
//         public void Test_1stPeriodCorrectEndDate_When3PeirodsAreDue()
//         {
//             int durationLength = 4;
//             int expectedNumDurationsCreated = 3;
//             BudgetPeriod lastPeriod = new BudgetPeriod(_builderFactoryFixture.BudgetBuilder.Build(), new DateTime(2020, 3, 1), new DateTime(2020, 3, 30));
//             DateTime dateToTest = lastPeriod.EndDate.AddDays(durationLength * expectedNumDurationsCreated);
//             DateTime firstPeriodExpectedEndDate = lastPeriod.EndDate.AddDays(durationLength);

//             BudgetDurationBase duration = ((DaySpanDurationBuilder) _builderFactoryFixture.GetService<BudgetDurationBuilderProvider>()
//                                             .GetBuilder<DaySpanDuration>())
//                                             .SetNumberDays(durationLength)
//                                             .Build();

//             UpdateBudgetPeriodsCommand subject = new UpdateBudgetPeriodsCommand();

//             IEnumerable<BudgetPeriod> created = subject.Run(lastPeriod, duration, dateToTest);

//             BudgetPeriod firstPeriod = created.First();

//             Assert.Equal(firstPeriodExpectedEndDate, firstPeriod.EndDate);
//         }

//         public void Dispose()
//         {
//             _builderFactoryFixture.Dispose();
//         }
//     }
// }