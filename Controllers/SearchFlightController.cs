using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RestSharp;
using ApiToken.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using FlightsSearchEngineProject.Models;

using System.Reflection;


namespace FlightsSearchEngineProject.Controllers
{
    public class SearchFlightController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _bearerToken;
        private readonly string _clientId = "k7N4VAZgfMjj2AddmxGwxWJaDZ7vso3T";
        private readonly string _clientSecret = "XWANvdFVKYVbtVbH";

        // Constructor: Dependency Injection of HttpClient
        public SearchFlightController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // Action method for handling requests to /SearchFlight/Index
        // Action method for handling requests to /SearchFlight/Index
        public IActionResult Index()
        {
            return View();
        }

        // Added

        public class AmadeusLocationAutocompleteResponse
        {
            public Meta Meta { get; set; }
            public List<LocationData> Data { get; set; }
        }

        public class Meta
        {
            public int Count { get; set; }
            public Links Links { get; set; }
        }

        public class Links
        {
            public string Self { get; set; }
        }

        public class LocationData
        {
            public string Type { get; set; }
            public string SubType { get; set; }
            public string Name { get; set; }
            public string IataCode { get; set; }
            public Address Address { get; set; }
            public GeoCode GeoCode { get; set; }
        }

        public class Address
        {
            public string CountryCode { get; set; }
            public string StateCode { get; set; }
            // Add other address properties as needed
        }

        public class GeoCode
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }


        //

        private async Task<string> GetCityCodeAsync(string cityName)
        {
            try
            {
                if (string.IsNullOrEmpty(_bearerToken))
                {
                    _bearerToken = GetAccessToken();
                }

                // Set up authorization header with the Bearer token
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                // Corrected API endpoint URL
                var apiUrl = $"https://test.api.amadeus.com/v1/reference-data/locations/cities?keyword={cityName}";

                var response = await _httpClient.GetAsync(apiUrl);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // Output the content to the console
                Console.WriteLine($"API Response Content: {content}");

                var result = JsonConvert.DeserializeObject<AmadeusLocationAutocompleteResponse>(content);

                // Filter results to include only CITY type
                var cityResults = result.Data
                                .Where(entry => entry.Type == "location" && entry.SubType == "city" &&
                                                string.Equals(entry.Name, cityName, StringComparison.OrdinalIgnoreCase))
                                .ToList();
                Console.WriteLine($"cityResults Content: {cityResults}");

                if (cityResults.Any())
                {
                    // Assuming the first CITY result contains the city information
                    var cityResult = cityResults.First();

                    // Extract the city code or any other relevant information
                    return cityResult.IataCode;
                }
                else
                {
                    Console.WriteLine($"No CITY result found for {cityName}");
                    return null; // Return null or some other indication that the city code is not found
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error while autocompleting location: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON error while deserializing location autocomplete response: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while autocompleting location: {ex.Message}");
            }

            // Return null or some other indication if an exception occurs
            return null;
        }



        public async Task<IActionResult> GetFlights(FlightSearchModel searchModel, bool? TypeFilter)

        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", searchModel);
                }

                // Ensure the bearer token is available
                if (string.IsNullOrEmpty(_bearerToken))
                {
                    _bearerToken = GetAccessToken();
                }

                // Set up authorization header with the Bearer token
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                var apiKey = "k7N4VAZgfMjj2AddmxGwxWJaDZ7vso3T";
                var apiUrl = "https://test.api.amadeus.com/v2/shopping/flight-offers";

                // Convert city names to city codes using Amadeus Location Autocomplete API
                var originCityCode = await GetCityCodeAsync(searchModel.DepartureCity);
                var destinationCityCode = await GetCityCodeAsync(searchModel.ArrivalCity);

                Console.WriteLine($"Origin City Code: {originCityCode}");
                Console.WriteLine($"Destination City Code: {destinationCityCode}");

                // Construct the API request URL

                var requestUrl = $"{apiUrl}?originLocationCode={originCityCode}&destinationLocationCode={destinationCityCode}&departureDate={searchModel.DepartureDate:yyyy-MM-dd}&returnDate={searchModel.ReturnDate:yyyy-MM-dd}&adults={searchModel.NumberOfPassengers}&travelClass={searchModel.TravelClass}";
              




                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"API Response Content: {apiResponse}");

                var responseObject = JsonConvert.DeserializeObject<Flight>(apiResponse);

                // Extract the flight data from the response object
                var flights = responseObject?.Data;

                TempData["DepartureCity"] = searchModel.DepartureCity;
                TempData["ArrivalCity"] = searchModel.ArrivalCity;
                TempData["NumberOfPassengers"] = searchModel.NumberOfPassengers;
                TempData["DepartureDate"] = searchModel.DepartureDate;
                TempData["ReturnDate"] = searchModel.ReturnDate;
                TempData["TravelClass"] = searchModel.TravelClass;



                Console.WriteLine($"flights content: {flights}");

                // Pass the flights data to the partial view
                return View("Index", flights);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error while retrieving flights: {ex.Message}");
                return BadRequest("Error while retrieving flights.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON error while deserializing flights: {ex.Message}");
                return BadRequest("Error while deserializing flights.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while retrieving flights: {ex.Message}");
                return BadRequest("Error while retrieving flights.");
            }
        }




        // Helper method to obtain or refresh the Bearer token
        [Obsolete]
        public string GetAccessToken()
        {
            var client = new RestClient("https://test.api.amadeus.com/v1/security/oauth2/token");
            var request = new RestRequest("v1/security/oauth2/token", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"))}");

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                ClientResponse bearerData = JsonConvert.DeserializeObject<ClientResponse>(response.Content);
                Console.WriteLine("Authorized successfully here is your token : " + bearerData.access_token);
                return bearerData.access_token;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}, {response.Content}");
                return null;
            }
        }
    }
}
