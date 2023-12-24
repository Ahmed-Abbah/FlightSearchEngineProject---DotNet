using System.ComponentModel.DataAnnotations;

namespace FlightsSearchEngineProject.Models
{
    public class FlightSearchModel
    {
        [Required(ErrorMessage = "Departure city is required")]
        public string DepartureCity { get; set; }

        [Required(ErrorMessage = "Arrival city is required")]
        public string ArrivalCity { get; set; }

        [Required(ErrorMessage = "Departure date is required")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Return date is required")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Number of passengers is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number of passengers")]
        public int NumberOfPassengers { get; set; }

        [Required(ErrorMessage = "Travel class is required")]
        public string TravelClass { get; set; }


        public bool? nonStop { get; set; }
    }
}