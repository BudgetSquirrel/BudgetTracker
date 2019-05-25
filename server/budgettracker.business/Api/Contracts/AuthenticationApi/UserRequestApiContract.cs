using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.AuthenticationApi
{
    public class UserRequestApiContract : IApiContract
    {
        [JsonProperty("first-name")]
        public string FirstName { get; set; }

        [JsonProperty("last-name")]
        public string LastName { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("password-confirm")]
        public string PasswordConfirm { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}