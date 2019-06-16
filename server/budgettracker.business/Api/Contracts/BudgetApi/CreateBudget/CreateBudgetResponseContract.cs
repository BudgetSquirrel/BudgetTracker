using budgettracker.business.Api.Contracts.BudgetApi.BudgetDurations;
using System;
using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi.CreateBudget
{
    public class CreateBudgetResponseContract : IApiContract {

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

    }
}
