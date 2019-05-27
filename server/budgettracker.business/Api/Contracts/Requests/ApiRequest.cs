using budgettracker.business.Api;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace budgettracker.business.Api.Contracts.Requests
{
    /// <summary>
    /// <p>
    /// Represents a request message coming in.
    /// </p>
    /// </summary>
    public class ApiRequest
    {
        [JsonProperty("user")]
        public UserRequestApiContract User { get; set; }

        [JsonProperty("arguments")]
        public Dictionary<string, object> ArgumentsDict { get; set; }

        public C Arguments<C>() where C : IApiContract {
            string argumentsRaw = JsonConvert.SerializeObject(ArgumentsDict);
            return JsonConvert.DeserializeObject<C>(argumentsRaw);
        }
    }
}