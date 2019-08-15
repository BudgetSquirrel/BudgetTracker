using BudgetTracker.Business.Api.Messages;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Messages.BudgetApi.GetBudget
{
    public class GetBudgetArgumentApiMessage : IApiMessage
    {
        [JsonProperty("budget-values")]
        public GetBudgetRequestMessage BudgetValues { get; set; }
    }
}
