using System;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Messages.BudgetApi.GetBudget
{
    public class GetBudgetRequestMessage : IApiMessage
    {
        /// <summary>
        /// Unique numeric identifier for this <see cref="Budget" />.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
