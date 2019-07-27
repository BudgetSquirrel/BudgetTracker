using System;

using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations;

using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget
{
    public class UpdateBudgetResponseContract : IApiContract {

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("percent-amount")]
        public double? PercentAmount { get; set; }

        [JsonProperty("set-amount")]
        public decimal SetAmount { get; set; }

        [JsonProperty("duration")]
        public BudgetDurationBaseContract Duration { get; set; }

        [JsonProperty("budget-start")]
        public DateTime BudgetStart { get; set; }

        [JsonProperty("parent-budget-id")]
        public Guid? ParentBudgetId { get; set; }

    }
}
