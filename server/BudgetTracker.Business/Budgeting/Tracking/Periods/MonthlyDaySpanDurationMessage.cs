using System;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Budgeting.Tracking.Periods
{
    public class MonthlyDaySpanDurationMessage : BudgetDurationBaseMessage
    {
        /// <summary>
        /// <p>
        /// Specifies the number of days that this budget will span.
        /// </p>
        /// </summary>
        [JsonProperty("number-days")]
        public int NumberDays { get; set; }
    }
}
