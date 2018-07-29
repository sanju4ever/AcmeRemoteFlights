using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public class Flight: VehicleBase
    {
        [Key]
        public override int Id { get; set; }

        [Required, MaxLength(5)]
        public override string Number { get; set; }

        [Required, MaxLength(20)]
        public override string Name { get; set; }

        [Required, MaxLength(20)]
        public string DepartureCity { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required, MaxLength(20)]
        public string ArrivalCity { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public short PassengerCapacity { get; set; }
    }
}
