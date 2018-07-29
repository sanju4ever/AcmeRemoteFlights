using System;
using System.Linq;
using AcmeRemoteFlights.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcmeRemoteFlights.Test
{
    [TestClass]
    public class BookingServiceTest
    {
        private readonly string dbConnectionString = "Data Source=DESKTOP-L4S9BBC\\SQLEXPRESS;Initial Catalog=AcmeRemoteFlights;Integrated Security=True";

        [TestMethod]
        public void Booking_AddNew_Seats_Available()
        {
            BookingRepository repository = new BookingRepository(dbConnectionString);

            var rndDays = new Random().Next(1, 30);

            Booking booking = new Booking
            {
                TravelDate = DateTime.Now.Date.AddDays(rndDays),
                FlightId = 8,
                PassengerId = 6,
                PassengerCount = 1
            };

            var result = repository.Add(booking);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Booking_Find()
        {
            BookingRepository repository = new BookingRepository(dbConnectionString);

            var result = repository.Find(DateTime.Parse("2018-8-8"), 1, 7);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.PassengerCount > 0);
        }
    }
}
