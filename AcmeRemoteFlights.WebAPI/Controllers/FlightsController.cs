using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeRemoteFlights.Services.Models;
using AcmeRemoteFlights.Services.Repositories;
using AcmeRemoteFlights.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AcmeRemoteFlights.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Flights")]
    public class FlightsController : Controller
    {
        IConfiguration _configuration;
        private readonly string conString;

        FlightRepository flightRepository;
        PassengerRepository passengerRepository;
        FlightBookingRepository flightBookingRepository;
        BookingRepository bookingRepository;

        public FlightsController(IConfiguration configuration)
        {
            _configuration = configuration;
            conString = configuration.GetSection("ConnectionString").Value;

            flightRepository = new FlightRepository(conString);
            passengerRepository = new PassengerRepository(conString);
            flightBookingRepository = new FlightBookingRepository(conString);
            bookingRepository = new BookingRepository(conString);
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok("AcmeRemoteFlights.WebAPI is ready!");
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Flight> flightList;

            try
            {
                flightList = flightRepository.GetAll();

                if (flightList == null) return NoContent();

                if (flightList.ToList().Count == 0) return NoContent();
            }
            catch
            {
                return BadRequest();
            }

            return Ok(flightList);
        }

        [HttpPost("CheckAvailability")]
        public IActionResult CheckAvailability([FromBody] AvailabilityRequest availabilityRequest)
        {
            if (availabilityRequest.StartDate > availabilityRequest.EndDate)
                return BadRequest("Invalid date range.");

            var bookingList = flightBookingRepository.Find(availabilityRequest.StartDate, availabilityRequest.EndDate);
            if (bookingList == null || bookingList.ToList().Count <= 0)
                return BadRequest("No Flights Available.");

            var flightList = flightRepository.GetAll();
            if (flightList == null || flightList.ToList().Count <= 0)
                return BadRequest("No Flights Available.");

            IList<AvailabilityResponseDTO> availabilityResponse = new List<AvailabilityResponseDTO>();

            foreach (FlightBooking booking in bookingList)
            {
                Flight flight = flightList.First(f => f.Id == booking.FlightId);
                if ((flight.PassengerCapacity - booking.PassengerCount) <= 0) continue;

                var availability = new AvailabilityResponseDTO
                {
                    FlightNumber = flight.Number,
                    FlightName = flight.Name,
                    TravelDate = booking.TravelDate,
                    DepartureCity = flight.DepartureCity,
                    DepartureTime = flight.DepartureTime,
                    ArrivalCity = flight.ArrivalCity,
                    ArrivalTime = flight.ArrivalTime,
                    AvailableSeats = (short)(flight.PassengerCapacity - booking.PassengerCount)
                };

                availabilityResponse.Add(availability);
            }

            return Ok(availabilityResponse);
        }
    }
}