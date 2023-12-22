using Newtonsoft.Json;
using System;

namespace ApiToken.Models
{
    public class ApiTokenModel
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("application_name")]
        public string ApplicationName { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        public DateTime CreateTime { get; init; }
        public DateTime ExpirationTime => CreateTime.AddSeconds(ExpiresIn).ToLocalTime();

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

        public ApiTokenModel()
        {
            CreateTime = DateTime.Now;
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
}
