using System;
using System.Linq;
using AcmeRemoteFlights.Services.Models;
using AcmeRemoteFlights.Services.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcmeRemoteFlights.Test
{
    [TestClass]
    public class FlightBookingServiceTest
    {
        private readonly string dbConnectionString = "Data Source=DESKTOP-L4S9BBC\\SQLEXPRESS;Initial Catalog=AcmeRemoteFlights;Integrated Security=True";

        [TestMethod]
        public void FlightBooking_AddNew()
        {
            FlightBookingRepository repository = new FlightBookingRepository(dbConnectionString);

            var rndDays = new Random().Next(1, 30);

            FlightBooking flightBooking = new FlightBooking
            {
                TravelDate = DateTime.Now.Date.AddDays(rndDays),
                FlightId = 8,
                PassengerCount = 2
            };

            var result = repository.Add(flightBooking);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void FlightBooking_FindByFlightAndDate()
        {
            FlightBookingRepository repository = new FlightBookingRepository(dbConnectionString);

            var result = repository.Find(1, DateTime.Parse("2018-8-8"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.FlightBookingId > 0);
        }

        [TestMethod]
        public void FlightBooking_FindByDate()
        {
            FlightBookingRepository repository = new FlightBookingRepository(dbConnectionString);

            var result = repository.Find(DateTime.Parse("2018-8-8"), DateTime.Parse("2018-8-12"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [TestMethod]
        public void FlightBooking_Update_PassengerCount()
        {
            FlightBookingRepository repository = new FlightBookingRepository(dbConnectionString);

            FlightBooking flightBooking = new FlightBooking
            {
                FlightId = 1,
                TravelDate = DateTime.Parse("2018-8-8"),
                PassengerCount = 2
            };
            var result = repository.Update(flightBooking);

            Assert.AreEqual(1, result);
        }
    }
}
