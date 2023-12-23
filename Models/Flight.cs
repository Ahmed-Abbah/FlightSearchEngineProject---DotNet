using Newtonsoft.Json;

namespace FlightsSearchEngineProject.Models
{
    public class Flight
    {
        [JsonProperty("data")]
        public List<FlightData> Data { get; set; }


        public class FlightData
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("source")]
            public string Source { get; set; }

            [JsonProperty("instantTicketingRequired")]
            public bool InstantTicketingRequired { get; set; }

            [JsonProperty("nonHomogeneous")]
            public bool NonHomogeneous { get; set; }

            [JsonProperty("oneWay")]
            public bool OneWay { get; set; }

            [JsonProperty("lastTicketingDate")]
            public string LastTicketingDate { get; set; }

            [JsonProperty("lastTicketingDateTime")]
            public string LastTicketingDateTime { get; set; }

            [JsonProperty("numberOfBookableSeats")]
            public int NumberOfBookableSeats { get; set; }

            [JsonProperty("itineraries")]
            public List<Itinerary> Itineraries { get; set; }

            [JsonProperty("price")]
            public Price Price { get; set; }

            [JsonProperty("pricingOptions")]
            public PricingOptions PricingOptions { get; set; }

            [JsonProperty("validatingAirlineCodes")]
            public List<string> ValidatingAirlineCodes { get; set; }

            [JsonProperty("travelerPricings")]
            public List<TravelerPricing> TravelerPricings { get; set; }
        }

        public class Itinerary
        {
            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("segments")]
            public List<Segment> Segments { get; set; }
        }

        public class Segment
        {
            [JsonProperty("departure")]
            public AirportInfo Departure { get; set; }

            [JsonProperty("arrival")]
            public AirportInfo Arrival { get; set; }

            [JsonProperty("carrierCode")]
            public string CarrierCode { get; set; }

            [JsonProperty("number")]
            public string Number { get; set; }

            [JsonProperty("aircraft")]
            public Aircraft Aircraft { get; set; }

            [JsonProperty("operating")]
            public Operating Operating { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("numberOfStops")]
            public int NumberOfStops { get; set; }

            [JsonProperty("blacklistedInEU")]
            public bool BlacklistedInEU { get; set; }
        }

        public class AirportInfo
        {
            [JsonProperty("iataCode")]
            public string IataCode { get; set; }

            [JsonProperty("terminal")]
            public string Terminal { get; set; }

            [JsonProperty("at")]
            public string At { get; set; }
        }

        public class Aircraft
        {
            [JsonProperty("code")]
            public string Code { get; set; }
        }

        public class Operating
        {
            [JsonProperty("carrierCode")]
            public string CarrierCode { get; set; }
        }

        public class Price
        {
            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("total")]
            public string Total { get; set; }

            [JsonProperty("base")]
            public string Base { get; set; }

            [JsonProperty("fees")]
            public List<Fee> Fees { get; set; }

            [JsonProperty("grandTotal")]
            public string GrandTotal { get; set; }

            [JsonProperty("additionalServices")]
            public List<AdditionalService> AdditionalServices { get; set; }
        }

        public class Fee
        {
            [JsonProperty("amount")]
            public string Amount { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class AdditionalService
        {
            [JsonProperty("amount")]
            public string Amount { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class PricingOptions
        {
            [JsonProperty("fareType")]
            public List<string> FareType { get; set; }

            [JsonProperty("includedCheckedBagsOnly")]
            public bool IncludedCheckedBagsOnly { get; set; }
        }

        public class TravelerPricing
        {
            [JsonProperty("travelerId")]
            public string TravelerId { get; set; }

            [JsonProperty("fareOption")]
            public string FareOption { get; set; }

            [JsonProperty("travelerType")]
            public string TravelerType { get; set; }

            [JsonProperty("price")]
            public Price Price { get; set; }

            [JsonProperty("fareDetailsBySegment")]
            public List<FareDetailsBySegment> FareDetailsBySegment { get; set; }
        }

        public class FareDetailsBySegment
        {
            [JsonProperty("segmentId")]
            public string SegmentId { get; set; }

            [JsonProperty("cabin")]
            public string Cabin { get; set; }

            [JsonProperty("fareBasis")]
            public string FareBasis { get; set; }

            [JsonProperty("brandedFare")]
            public string BrandedFare { get; set; }

            [JsonProperty("brandedFareLabel")]
            public string BrandedFareLabel { get; set; }

            [JsonProperty("class")]
            public string Class { get; set; }

            [JsonProperty("includedCheckedBags")]
            public BaggageInfo IncludedCheckedBags { get; set; }

            [JsonProperty("amenities")]
            public List<Amenity> Amenities { get; set; }
        }

        public class BaggageInfo
        {
            [JsonProperty("quantity")]
            public int Quantity { get; set; }
        }

        public class Amenity
        {
            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("isChargeable")]
            public bool IsChargeable { get; set; }

            [JsonProperty("amenityType")]
            public string AmenityType { get; set; }

            [JsonProperty("amenityProvider")]
            public AmenityProvider AmenityProvider { get; set; }
        }

        public class AmenityProvider
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }


    }
}
