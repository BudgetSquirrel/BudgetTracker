using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class CreateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-value")]
        public CreateBudgetRequestContract BudgetValue { get; set; }
    }
}