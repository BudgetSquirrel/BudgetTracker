using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi
{
    public class DeleteBudgetArgumentsApiMessage : IApiMessage
    {
        [JsonProperty("budget-ids")]
        public List<Guid> BudgetIds { get; set; }
    }
}
