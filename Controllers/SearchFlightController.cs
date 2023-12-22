using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RestSharp;
using ApiToken.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FlightsSearchEngineProject.Models;
using System.Text;

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
        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://test.api.amadeus.com/v1/shopping/flight-destinations?origin=MAD";

            try
            {
                // Obtain or refresh Bearer token
                if (string.IsNullOrEmpty(_bearerToken))
                {
                    _bearerToken = GetAccessToken();
                }

                // Set up authorization header with the Bearer token
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                // Sending a GET request to the specified API endpoint
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                // Check if the response is successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Received JSON content: " + content);

                    // Deserialize the JSON content into a FlightDestinationsResponse
                    var responseModel = JsonConvert.DeserializeObject<FlightDestinationsResponse>(content);

                    // Check if the responseModel has data
                    if (responseModel?.Data != null && responseModel.Data.Any())
                    {
                        // Pass the list of ApiTokenModel to the view
                        return View(responseModel.Data);
                    }
                }
                else
                {
                    ViewData["ApiError"] = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ApiError"] = $"Exception: {ex.Message}";
            }

            // If something goes wrong, return the view with an empty list
            return View(new List<ApiTokenModel>());
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
