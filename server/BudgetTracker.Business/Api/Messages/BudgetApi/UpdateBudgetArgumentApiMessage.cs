using BudgetTracker.Business.Budgeting;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Messages.BudgetApi
{
    public class UpdateBudgetArgumentApiMessage : IApiMessage
    {
        [JsonProperty("budget-values")]
        public UpdateBudgetRequestMessage BudgetValues { get; set; }
    }
}
