using BudgetTracker.Business.Api.Messages;
using BudgetTracker.Business.Auth;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Messages.AuthenticationApi
{
    public class LoginArgumentsApiMessage : IApiMessage
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
