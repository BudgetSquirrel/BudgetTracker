using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Auth
{
    public class UserResponseApiMessage
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

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
