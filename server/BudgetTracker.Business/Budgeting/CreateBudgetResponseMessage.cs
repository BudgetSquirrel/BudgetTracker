using BudgetTracker.Business.Api.Contracts;
using BudgetTracker.Business.Budgeting.Tracking.Periods;
using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Budgeting
{
    public class CreateBudgetResponseMessage : IApiContract {

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("percent-amount")]
        public double? PercentAmount { get; set; }

        [JsonProperty("set-amount")]
        public decimal SetAmount { get; set; }

        [JsonProperty("duration")]
        public BudgetDurationBaseMessage Duration { get; set; }

        [JsonProperty("budget-start")]
        public DateTime BudgetStart { get; set; }

        [JsonProperty("parent-budget-id")]
        public Guid? ParentBudgetId { get; set; }

    }
}
