using budgettracker.business.Api;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.Requests;
using Newtonsoft.Json;
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

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("arguments")]
        public Dictionary<string, object> ArgumentsDict { get; set; }

        public C Arguments<C>() where C : IApiContract {
            ArgumentsRaw = ArgumentsRaw == null ?
                JsonConvert.SerializeObject(ArgumentsDict) :
                ArgumentsRaw;
            return JsonConvert.DeserializeObject<C>(ArgumentsRaw);
        }

        /// <summary>
        /// Cached value for arguments so that we can get it as an object or a dictionary
        /// as needed without reserializing and deserializing.
        /// </summary>
        private string ArgumentsRaw { get; set; }
    }
}