using Newtonsoft.Json;

namespace BudgetTracker.Business.Api.Contracts.AuthenticationApi
{
    public class UserRegistrationArgumentApiContract : IApiContract
    {
        [JsonProperty("user-values")]
        public UserRequestApiContract UserValues { get; set; }
    }
}