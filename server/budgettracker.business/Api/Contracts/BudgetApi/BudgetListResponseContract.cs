using budgettracker.business.Api.Contracts;
using budgettracker.business.Api.Contracts.BudgetApi.BudgetDurations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class BudgetListResponseContract : IApiContract
    {
        [JsonProperty("budgets")]
        public List<BudgetResponseContract> Budgets { get; set; }
    }
}
