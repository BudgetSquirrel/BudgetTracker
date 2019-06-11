using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi.CreateBudget
{
    public class CreateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-value")]
        public CreateBudgetRequestContract BudgetValue { get; set; }
    }
}