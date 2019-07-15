using BudgetTracker.Business.Api.Contracts;
using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetTree
{
    public class FetchBudgetTreeArgumentsApiContract : IApiContract
    {
        [JsonProperty("root-budget-id")]
        public Guid RootBudgetId { get; set; }
    }
}
