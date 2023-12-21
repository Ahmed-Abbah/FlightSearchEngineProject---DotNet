using Newtonsoft.Json;

namespace FlightsSearchEngineProject.Models
{
    public class FlightDestinationModel
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("departureDate")]
        public string DepartureDate { get; set; }

        [JsonProperty("returnDate")]
        public string ReturnDate { get; set; }

        [JsonProperty("price")]
        public PriceModel Price { get; set; }

        [JsonProperty("links")]
        public LinksModel Links { get; set; }
    }

    public class PriceModel
    {
        [JsonProperty("total")]
        public string Total { get; set; }
    }

    public class LinksModel
    {
        [JsonProperty("flightDates")]
        public string FlightDates { get; set; }

        [JsonProperty("flightOffers")]
        public string FlightOffers { get; set; }
    }

}
