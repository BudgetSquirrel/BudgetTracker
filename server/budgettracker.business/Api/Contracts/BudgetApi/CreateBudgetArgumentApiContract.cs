using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class CreateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("new-budget")]
        public CreateBudgetRequestContract BudgetValue { get; set; }
    }
}