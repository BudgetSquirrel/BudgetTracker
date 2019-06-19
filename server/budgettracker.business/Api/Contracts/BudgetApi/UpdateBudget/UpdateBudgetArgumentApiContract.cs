using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi.UpdateBudget
{
    public class UpdateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public UpdateBudgetRequestContract BudgetValues { get; set; }
    }
}