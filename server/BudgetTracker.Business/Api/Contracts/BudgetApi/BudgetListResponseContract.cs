using BudgetTracker.Business.Api.Contracts;
using BudgetTracker.Business.Budgeting.BudgetPeriods;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi
{
    public class BudgetListResponseContract : IApiContract
    {
        [JsonProperty("budgets")]
        public List<BudgetResponseMessage> Budgets { get; set; }
    }
}
