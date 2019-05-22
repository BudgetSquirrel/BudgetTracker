using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.AuthenticationApi
{
    public class UserRegistrationArgumentApiContract : IApiContract
    {
        [JsonProperty("user-values")]
        public UserRequestApiContract UserValues { get; set; }
    }
}