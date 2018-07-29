using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public class Booking
    {
        [Required]
        public DateTime TravelDate { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required]
        public int PassengerId { get; set; }

        [Required, Range(1,4)]
        public short PassengerCount { get; set; }

        public DateTime DateBooked { get; }

        public string Notes { get; set; }
    }
}
