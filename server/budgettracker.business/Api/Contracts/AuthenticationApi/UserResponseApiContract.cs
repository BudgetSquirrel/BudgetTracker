using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.AuthenticationApi
{
    public class UserResponseApiContract : IApiContract
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("first-name")]
        public string FirstName { get; set; }

        [JsonProperty("last-name")]
        public string LastName { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
