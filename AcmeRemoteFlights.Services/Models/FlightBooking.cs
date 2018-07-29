using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public class FlightBooking
    {
        [Key]
        public int FlightBookingId { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required]
        public DateTime TravelDate { get; set; }

        [Required]
        public short PassengerCount { get; set; }
    }
}
