using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget
{
    public class UpdateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public UpdateBudgetRequestContract BudgetValues { get; set; }
    }
}