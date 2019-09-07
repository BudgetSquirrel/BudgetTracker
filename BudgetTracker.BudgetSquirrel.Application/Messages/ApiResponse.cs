using Newtonsoft.Json;

namespace BudgetTracker.BudgetSquirrel.Application.Messages
{
    public class ApiResponse
    {
        /// <summary>
        /// <p>
        /// Instantiate an empty response. This designates that the request was
        /// good and succeeded but no data was provided to be returned. Think of
        /// this as a void return value.
        /// </p>
        /// </summary>
        public ApiResponse()
        {}

        public ApiResponse(object data)
        {
            Response = data;
        }

        public ApiResponse(string error)
        {
            Error = error;
        }

        [JsonProperty("response", NullValueHandling=NullValueHandling.Ignore)]
        public object Response { get; set; }

        [JsonProperty("error", NullValueHandling=NullValueHandling.Ignore)]
        public string Error { get; set; }
    }
}
