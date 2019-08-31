using BudgetTracker.Business.Budgeting;
using BudgetTracker.TestUtils.Budgeting;
using System;
using Xunit;

namespace BudgetTracker.BudgetSquirrel.Application.Tests
{
    public class BudgetCreationTests
    {
        [Fact]
        public void TestBudgetSavedWhenValidRequestReceived()
        {
            BudgetBuilder bb = new BudgetBuilderFactory<Budget>().GetBuilder() as BudgetBuilder;
            Budget budget = bb.Build();
            Console.WriteLine(budget.Duration.Id);
        }
    }
}
