using System;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.GetBudget
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
