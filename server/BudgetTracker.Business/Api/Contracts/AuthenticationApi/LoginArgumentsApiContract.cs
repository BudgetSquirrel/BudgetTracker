using BudgetTracker.Business.Api.Contracts;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.AuthenticationApi
{
    public class LoginArgumentsApiContract : IApiContract
    {
        /// <summary>
        /// Contains the username and password for logging in.
        /// All other properties on the <see cref="UserRequestApiContract" />
        /// will be ignored.
        /// </summary>
        [JsonProperty("credentials")]
        public UserRequestApiContract Credentials { get; set; }
    }
}