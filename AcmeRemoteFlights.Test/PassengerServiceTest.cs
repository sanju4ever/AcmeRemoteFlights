using System;
using System.Linq;
using AcmeRemoteFlights.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcmeRemoteFlights.Test
{
    [TestClass]
    public class PassengerServiceTest
    {
        private readonly string dbConnectionString = "Data Source=DESKTOP-L4S9BBC\\SQLEXPRESS;Initial Catalog=AcmeRemoteFlights;Integrated Security=True";

        [TestMethod]
        public void Passenger_AddNew()
        {
            PassengerRepository repository = new PassengerRepository(dbConnectionString);

            var rndNo = new Random().Next(10, 999999);

            Passenger passenger = new Passenger
            {
                IdentityNumber = "PASSENGER-" + rndNo.ToString(),
                FirstName = "First Name",
                LastName = "Last Name"
            };

            var result = repository.Add(passenger);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Passenger_FindById()
        {
            PassengerRepository repository = new PassengerRepository(dbConnectionString);
            var passenger = repository.Find(1);

            Assert.IsNotNull(passenger);
            Assert.AreEqual("23456-RT-334867", passenger.IdentityNumber);
            Assert.AreEqual("Andrew", passenger.FirstName);
            Assert.AreEqual("Parker", passenger.LastName);
        }
    }
}
