using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Budgeting.BudgetPeriods;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Messages.BudgetApi
{
    public class CreateBudgetArgumentApiMessage : IApiMessage
    {
        [JsonProperty("budget-values")]
        public CreateBudgetRequestMessage BudgetValues { get; set; }
    }
}
