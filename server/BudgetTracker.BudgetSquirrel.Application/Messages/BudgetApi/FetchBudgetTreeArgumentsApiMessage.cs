using BudgetTracker.BudgetSquirrel.Application.Messages;
using Newtonsoft.Json;
using System;

namespace BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi
{
    public class FetchBudgetTreeArgumentsApiMessage : IApiMessage
    {
        [JsonProperty("root-budget-id")]
        public Guid RootBudgetId { get; set; }
    }
}
