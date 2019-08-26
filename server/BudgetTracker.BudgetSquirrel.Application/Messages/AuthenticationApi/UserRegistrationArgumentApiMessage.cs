using BudgetTracker.Business.Auth;
using Newtonsoft.Json;

namespace BudgetTracker.BudgetSquirrel.Application.Messages.AuthenticationApi
{
    public class UserRegistrationArgumentApiMessage : IApiMessage
    {
        [JsonProperty("user-values")]
        public UserRequestApiMessage UserValues { get; set; }
    }
}
