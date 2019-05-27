using Newtonsoft.Json;
using System;
using budgettracker.business.Api.Contracts.AuthenticationApi;


namespace budgettracker.business.Api.Contracts.BudgetApi
{
    public class BudgetResquestContract : IApiContract
    {
        [JsonProperty("user")]
        public User

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("set-amount")]
        public decimal SetAmount { get; set; }

        /// <summary>
        /// Duration is handled in days
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }
    }
}