using BudgetTracker.Business.Api.Contracts;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi
{
    public class BudgetListResponseContract : IApiContract
    {
        [JsonProperty("budgets")]
        public List<BudgetResponseContract> Budgets { get; set; }
    }
}
