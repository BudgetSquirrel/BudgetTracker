using BudgetTracker.Business.Api.Messages;
using BudgetTracker.Business.Budgeting.BudgetPeriods;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Messages.BudgetApi
{
    public class BudgetListResponseMessage : IApiMessage
    {
        [JsonProperty("budgets")]
        public List<BudgetResponseMessage> Budgets { get; set; }
    }
}
