using BudgetTracker.Business.Auth;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.AuthenticationApi
{
    public class UserRegistrationArgumentApiContract : IApiContract
    {
        [JsonProperty("user-values")]
        public UserRequestApiMessage UserValues { get; set; }
    }
}
