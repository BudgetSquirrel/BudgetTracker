using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace budgettracker.business.Api.Contracts.BudgetApi.DeleteBudgets
{
    public class DeleteBudgetArgumentsApiContract : IApiContract
    {
        [JsonProperty("budget-ids")]
        public List<Guid> BudgetIds { get; set; }
    }
}
