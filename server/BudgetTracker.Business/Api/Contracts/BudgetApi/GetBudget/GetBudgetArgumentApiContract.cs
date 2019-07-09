using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi.GetBudget
{
    public class GetBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public GetBudgetRequestContract BudgetValues { get; set; }
    }
}