using System;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Budgeting.BudgetPeriods
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
