using budgettracker.business.Api.Contracts.Responses;
using Newtonsoft.Json;

namespace budgettracker.business.Api.Contracts.Responses
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