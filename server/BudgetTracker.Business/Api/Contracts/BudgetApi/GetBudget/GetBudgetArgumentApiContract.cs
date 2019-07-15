using BudgetTracker.Business.Api.Contracts;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.GetBudget
{
    public class GetBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public GetBudgetRequestContract BudgetValues { get; set; }
    }
}
