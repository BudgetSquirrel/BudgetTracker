using BudgetTracker.Business.Auth;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Messages.AuthenticationApi
{
    public class UserRegistrationArgumentApiMessage : IApiMessage
    {
        [JsonProperty("user-values")]
        public UserRequestApiMessage UserValues { get; set; }
    }
}
