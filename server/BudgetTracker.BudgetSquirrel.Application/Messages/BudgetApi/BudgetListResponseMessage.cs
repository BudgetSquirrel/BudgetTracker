using BudgetTracker.BudgetSquirrel.Application.Messages;
using BudgetTracker.Business.BudgetPeriods;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi
{
    public class BudgetListResponseMessage : IApiMessage
    {
        [JsonProperty("budgets")]
        public List<BudgetResponseMessage> Budgets { get; set; }
    }
}
