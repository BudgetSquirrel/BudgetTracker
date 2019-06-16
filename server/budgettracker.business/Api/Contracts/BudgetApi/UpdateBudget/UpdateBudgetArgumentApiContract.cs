using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi.UpdateBudget
{
    public class UpdateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public UpdateBudgetRequestContract BudgetValue { get; set; }
    }
}