using BudgetTracker.Business.Api;
using BudgetTracker.Business.Api.Messages.AuthenticationApi;
using BudgetTracker.Business.Api.Messages.Requests;
using BudgetTracker.Business.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Messages.Requests
{
    /// <summary>
    /// <p>
    /// Represents a request message coming in.
    /// </p>
    /// </summary>
    public class ApiRequest
    {
        [JsonProperty("user")]
        public UserRequestApiMessage User { get; set; }

        [JsonProperty("arguments")]
        public Dictionary<string, object> ArgumentsDict { get; set; }

        public C Arguments<C>() where C : IApiMessage {
            string argumentsRaw = JsonConvert.SerializeObject(ArgumentsDict);
            return JsonConvert.DeserializeObject<C>(argumentsRaw);
        }
    }
}
