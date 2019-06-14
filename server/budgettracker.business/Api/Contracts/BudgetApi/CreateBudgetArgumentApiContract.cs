using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class CreateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public CreateBudgetRequestContract BudgetValues { get; set; }
    }
}
