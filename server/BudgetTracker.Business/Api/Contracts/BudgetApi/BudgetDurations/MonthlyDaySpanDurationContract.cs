using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations
{
    public class MonthlyDaySpanDurationContract : BudgetDurationBaseContract
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
