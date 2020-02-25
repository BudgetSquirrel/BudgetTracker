using System;

namespace BudgetTracker.Business.BudgetPeriods
{
    public class MonthlyDaySpanDuration : BudgetDurationBase
    {
        /// <summary>
        /// <p>
        /// Specifies the number of days that this budget will span.
        /// </p>
        /// </summary>
        public int NumberDays { get; set; }

        public override DateTime GetEndDateFromStartDate(DateTime startDate)
        {
            return startDate.AddDays(NumberDays);
        }
    }
}
