using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Messages.BudgetApi.DeleteBudgets
{
    public class DeleteBudgetArgumentsApiMessage : IApiMessage
    {
        [JsonProperty("budget-ids")]
        public List<Guid> BudgetIds { get; set; }
    }
}
