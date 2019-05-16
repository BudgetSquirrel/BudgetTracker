using budgettracker.business.Api.Contracts;
using Newtonsoft.Json;

namespace budgettracker.business.Api
{
    public class ApiResponse
    {
        public ApiResponse(IApiContract data)
        {
            Response = data;
        }

        public ApiResponse(string error)
        {
            Error = error;
        }

        [JsonProperty("response", NullValueHandling=NullValueHandling.Ignore)]
        public IApiContract Response { get; set; }
        
        [JsonProperty("error", NullValueHandling=NullValueHandling.Ignore)]
        public string Error { get; set; }
    }
}