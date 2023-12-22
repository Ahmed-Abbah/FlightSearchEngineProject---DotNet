using ApiToken.Models;
using Newtonsoft.Json;

namespace FlightsSearchEngineProject.Models
{
    public class FlightDestinationsResponse
    {
        [JsonProperty("data")]
        public List<ApiTokenModel> Data { get; set; }
    }
}
