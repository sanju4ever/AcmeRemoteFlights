using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeRemoteFlights.WebAPI.Models
{
    public class AvailabilityResponseDTO
    {
        public string FlightNumber { get; set; }

        public string FlightName { get; set; }

        public DateTime TravelDate { get; set; }

        public string DepartureCity { get; set; }

        public DateTime DepartureTime { get; set; }

        public string ArrivalCity { get; set; }

        public DateTime ArrivalTime { get; set; }

        public short AvailableSeats { get; set; }
    }
}
