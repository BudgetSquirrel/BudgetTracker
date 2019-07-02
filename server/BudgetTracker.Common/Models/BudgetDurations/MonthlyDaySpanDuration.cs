namespace BudgetTracker.Common.Models.BudgetDurations
{
    public class MonthlyDaySpanDuration : BudgetDurationBase
    {
        /// <summary>
        /// <p>
        /// Specifies the number of days that this budget will span.
        /// </p>
        /// </summary>
        public int NumberDays { get; set; }
    }
}
