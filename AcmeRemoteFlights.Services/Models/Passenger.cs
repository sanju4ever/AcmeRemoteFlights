using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public class Passenger
    {
        [Required, MaxLength(20)]
        public string IdentityNumber { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
