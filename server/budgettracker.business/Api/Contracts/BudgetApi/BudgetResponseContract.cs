using budgettracker.business.Api.Contracts.BudgetApi.BudgetDurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class BudgetResponseContract : IApiContract {

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("set-amount")]
        public decimal SetAmount { get; set; }

        [JsonProperty("duration")]
        public BudgetDurationBaseContract Duration { get; set; }

        [JsonProperty("budget-start")]
        public DateTime BudgetStart { get; set; }

        [JsonProperty("parent-budget-id")]
        public Guid? ParentBudgetId { get; set; }

        [JsonProperty("sub-budgets")]
        public List<BudgetResponseContract> SubBudgets { get; set; }
    }
}
