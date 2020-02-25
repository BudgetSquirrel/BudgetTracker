using System;

namespace BudgetSquirrel.Business.BudgetPlanning
{
    public class DaySpanDuration : BudgetDurationBase
    {
        /// <summary>
        /// <p>
        /// Specifies the number of days that this budget will span.
        /// </p>
        /// </summary>
        public int NumberDays { get; set; }
    }
}
