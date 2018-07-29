using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeRemoteFlights.WebAPI.Models
{
    public class SearchRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime TravelDate { get; set; }

        public string DepartureCity { get; set; }

        public string ArrivalCity { get; set; }

        public string FlightNumber { get; set; }
    }
}
