using BudgetTracker.Business.Budgeting.BudgetPeriods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi
{
    public class BudgetResponseMessage : IApiContract {

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

        [JsonProperty("sub-budgets")]
        public List<BudgetResponseMessage> SubBudgets { get; set; }
    }
}
