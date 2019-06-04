using System;
using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class CreateBudgetResponseContract : IApiContract {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("set-amount")]
        public decimal SetAmount { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("budget-start")]
        public DateTime BudgetStart { get; set; }
    }
}