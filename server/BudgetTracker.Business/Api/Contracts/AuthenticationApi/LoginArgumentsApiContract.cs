using BudgetTracker.Business.Api.Contracts;
using BudgetTracker.Business.Auth;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.AuthenticationApi
{
    public class LoginArgumentsApiContract : IApiContract
    {
        /// <summary>
        /// Contains the username and password for logging in.
        /// All other properties on the <see cref="UserRequestApiMessage" />
        /// will be ignored.
        /// </summary>
        [JsonProperty("credentials")]
        public UserRequestApiMessage Credentials { get; set; }
    }
}
