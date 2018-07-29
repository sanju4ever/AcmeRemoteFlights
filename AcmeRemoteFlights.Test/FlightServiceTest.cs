using System;
using System.Linq;
using AcmeRemoteFlights.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcmeRemoteFlights.Test
{
    [TestClass]
    public class FlightServiceTest
    {
        private readonly string dbConnectionString = "Data Source=DESKTOP-L4S9BBC\\SQLEXPRESS;Initial Catalog=AcmeRemoteFlights;Integrated Security=True";

        [TestMethod]
        public void Flight_GetAll()
        {
            FlightRepository repository = new FlightRepository(dbConnectionString);
            var flightList = repository.GetAll();
            var count = flightList.ToList().Count;

            Assert.IsNotNull(flightList);
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void Flight_FindById()
        {
            FlightRepository repository = new FlightRepository(dbConnectionString);
            var flight = repository.Find(1);

            Assert.IsNotNull(flight);
            Assert.AreEqual("FL001", flight.Number);
            Assert.AreEqual(6, flight.PassengerCapacity);
        }

        [TestMethod]
        public void Flight_AddNew()
        {
            FlightRepository repository = new FlightRepository(dbConnectionString);
            var flightList = repository.GetAll();
            var prevCount = flightList.ToList().Count;

            var rndNo = new Random().Next(10, 999);

            Flight flight = new Flight
            {
                Number = "FL" + rndNo.ToString(),
                Name = "Test Flight Name",
                DepartureCity = "Craigieburn",
                DepartureTime = DateTime.Now,
                ArrivalCity = "Melbourne",
                ArrivalTime = DateTime.Now.AddHours(4),
                PassengerCapacity = 20
            };

            var result = repository.Add(flight);
            flightList = repository.GetAll();
            var afterCount = flightList.ToList().Count;

            Assert.AreEqual(1, result);
            Assert.IsTrue(prevCount > 0);
            Assert.IsTrue(afterCount > 0);
            Assert.IsTrue(afterCount > prevCount);
        }
    }
}
