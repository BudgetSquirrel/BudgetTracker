using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget
{
    public class CreateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public CreateBudgetRequestContract BudgetValues { get; set; }
    }
}
