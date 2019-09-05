using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using Newtonsoft.Json;

namespace BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi
{
    public class CreateBudgetArgumentApiMessage : IApiMessage
    {
        [JsonProperty("budget-values")]
        public CreateBudgetRequestMessage BudgetValues { get; set; }
    }
}
