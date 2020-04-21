using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils.Budgeting
{
    public class BudgetDurationBuilderProvider
    {
        public IBudgetDurationBuilder GetBuilder<TDuration>()
        {
            if (typeof(TDuration) == typeof(MonthlyBookEndedDuration))
            {
                return new MonthlyBookEndedDurationBuilder();
            }
            else if (typeof(TDuration) == typeof(DaySpanDuration))
            {
                return new DaySpanDurationBuilder();
            }
            else
                return null;
        }
    }
}