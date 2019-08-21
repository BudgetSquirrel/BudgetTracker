using BudgetTracker.BudgetSquirrel.Application.Messages;
using Newtonsoft.Json;

namespace BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi
{
    public class GetBudgetArgumentApiMessage : IApiMessage
    {
        [JsonProperty("budget-values")]
        public GetBudgetRequestMessage BudgetValues { get; set; }
    }
}
