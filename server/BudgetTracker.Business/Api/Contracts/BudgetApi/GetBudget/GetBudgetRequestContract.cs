using System;
using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.BudgetApi.GetBudget
{
    public class GetBudgetRequestContract : IApiContract
    {
        /// <summary>
        /// Unique numeric identifier for this <see cref="Budget" />.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}