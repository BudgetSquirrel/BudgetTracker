using BudgetTracker.BudgetSquirrel.Application;
using BudgetTracker.BudgetSquirrel.Application.Messages.AuthenticationApi;
using BudgetTracker.Business.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BudgetTracker.BudgetSquirrel.Application.Messages
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

        public C Arguments<C>() {
            string argumentsRaw = JsonConvert.SerializeObject(ArgumentsDict);
            return JsonConvert.DeserializeObject<C>(argumentsRaw);
        }
    }
}
