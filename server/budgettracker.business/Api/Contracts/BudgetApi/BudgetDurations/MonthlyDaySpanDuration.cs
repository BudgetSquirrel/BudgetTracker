namespace budgettracker.business.Api.Contracts.BudgetApi.BudgetDurations
{
    public class MonthlyDaySpanDurationContract : BudgetDurationBaseContract
    {
        /// <summary>
        /// <p>
        /// Specifies the number of days that this budget will span.
        /// </p>
        /// </summary>
        public int NumDays { get; set; }
    }
}
