using Microsoft.AspNetCore.Mvc;

using System.Net.Http.Headers;
using Newtonsoft.Json;


using RestSharp;
using ApiToken.Models;
using FlightsSearchEngineProject.Models;
using RestSharp.Authenticators;
using System.Text;


namespace FlightsSearchEngineProject.Controllers
{
    public class SearchFlightController : Controller
    {
        private readonly HttpClient _httpClient;
        
        private string _bearerToken;
        public RestClient _client;
        public RestRequest _request;
        private readonly string _clientId = "OllFoRCGzYf2Gu7oK2H7dqeh3JEOEeg2";
        private readonly string _clientSecret = "F4X5O0uqMxkKmEkO";




        // Constructor: Dependency Injection of HttpClient
        public SearchFlightController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // Action method for handling requests to /SearchFlight/Index
        public async Task<IActionResult> Index()
        {
            // Replace with your actual API credentials
            

           

        // Replace this URL with the actual API endpoint you want to consume
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

               /* // Replace "MAD" with your actual API parameter value
                string apiParameter = "MAD";

                // Append the API parameter to the URL
                apiUrl += $"?origin={apiParameter}";*/

                // Sending a GET request to the specified API endpoint
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                // Check if the response is successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string content = await response.Content.ReadAsStringAsync();

                    // Store the API data in the ViewData dictionary, which can be accessed in the view
                    ViewData["ApiData"] = content;
                }
                else
                {
                    // If the response is not successful, store an error message in ViewData
                    ViewData["ApiError"] = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs during the request, store the exception message in ViewData
                ViewData["ApiError"] = $"Exception: {ex.Message}";
            }

            // Return the view associated with this action method
            return View();
        }

        // Helper method to obtain or refresh the Bearer token
        [Obsolete]
        public string GetAccessToken()
        {
            var client = new RestClient("https://test.api.amadeus.com/v1/security/oauth2/token");

            // Set the authenticator directly on the request
            var request = new RestRequest("v1/security/oauth2/token", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");

            // Set the Basic Authentication header directly on the request
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"))}");

            // Execute the request
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                // Deserialize the response directly into an object
                ClientResponse bearerData = JsonConvert.DeserializeObject<ClientResponse>(response.Content);
                Console.WriteLine("Authorized successfully here is your token : " + bearerData.access_token);
                return bearerData.access_token;
            }
            else
            {
                // Handle errors or log the response details
                Console.WriteLine($"Error: {response.StatusCode}, {response.Content}");
                return null;
            }
        }





    }
}
