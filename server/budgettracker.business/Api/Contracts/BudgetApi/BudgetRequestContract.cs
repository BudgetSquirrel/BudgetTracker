using Newtonsoft.Json;
using System;

namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class BudgetResquestContract : IApiContract
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SetAmount")]
        public decimal SetAmount { get; set; }

        /// <summary>
        /// Duration is handled in days
        /// </summary>
        [JsonProperty("Duration")]
        public int Duration { get; set; }

        [JsonProperty("DurationStart")]
        public DateTime DurationStart { get; set; }

    }
}