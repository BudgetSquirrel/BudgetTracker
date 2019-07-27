using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Budgeting.Tracking.Periods;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget
{
    public class CreateBudgetArgumentApiContract : IApiContract
    {
        [JsonProperty("budget-values")]
        public CreateBudgetRequestMessage BudgetValues { get; set; }
    }
}
