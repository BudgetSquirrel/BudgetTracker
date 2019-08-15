using BudgetTracker.Business.Api.Messages;
using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Api.Messages.BudgetApi.BudgetTree
{
    public class FetchBudgetTreeArgumentsApiMessage : IApiMessage
    {
        [JsonProperty("root-budget-id")]
        public Guid RootBudgetId { get; set; }
    }
}
