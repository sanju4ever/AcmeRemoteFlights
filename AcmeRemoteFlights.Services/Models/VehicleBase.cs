using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public abstract class VehicleBase
    {
        public abstract int Id { get; set; }

        public abstract string Number { get; set; }

        public abstract string Name { get; set; }
    }
}
