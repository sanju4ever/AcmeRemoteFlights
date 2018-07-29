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
    [Route("api/Bookings")]
    public class BookingsController : Controller
    {
        IConfiguration _configuration;
        private readonly string conString;

        FlightRepository flightRepository;
        PassengerRepository passengerRepository;
        FlightBookingRepository flightBookingRepository;
        BookingRepository bookingRepository;

        public BookingsController(IConfiguration configuration)
        {
            _configuration = configuration;
            conString = configuration.GetSection("ConnectionString").Value;

            flightRepository = new FlightRepository(conString);
            passengerRepository = new PassengerRepository(conString);
            flightBookingRepository = new FlightBookingRepository(conString);
            bookingRepository = new BookingRepository(conString);
        }

        [HttpPost("Book")]
        public IActionResult Book([FromBody] Booking bookingRequest)
        {
            if (!(bookingRequest.PassengerCount > 0)) return BadRequest("At least 1 passenger is required.");

            Flight flight = flightRepository.Find(bookingRequest.FlightId);
            if (flight == null || flight.PassengerCapacity <= 0) return BadRequest("Invalid Flight.");

            Passenger passenger = passengerRepository.Find(bookingRequest.PassengerId);
            if (passenger == null || string.IsNullOrWhiteSpace(passenger.IdentityNumber)) return BadRequest("Invalid Passenger.");

            if (bookingRequest.PassengerCount > flight.PassengerCapacity) return BadRequest("Passenger Capacity exceeded.");

            Booking booking = bookingRepository.Find(bookingRequest.TravelDate, bookingRequest.FlightId, bookingRequest.PassengerId);
            if (booking != null && booking.PassengerCount > 0)
            {
                return BadRequest("Flight already booked for the same travel day by the same passenger.");
            }
            else
            {
                // Valid booking request, so proceed.
                bookingRepository.Add(bookingRequest);
            }

            FlightBooking flightBooking = flightBookingRepository.Find(bookingRequest.FlightId, bookingRequest.TravelDate);         
            if (flightBooking != null && flightBooking.FlightBookingId > 0)
            {
                var currentPassengerCount = flightBooking.PassengerCount;

                if ((currentPassengerCount + bookingRequest.PassengerCount) > flight.PassengerCapacity)
                    return BadRequest("Passenger Capacity exceeded.");
                else
                {
                    // Valid passenger count.
                    flightBooking.PassengerCount = (short)(currentPassengerCount + bookingRequest.PassengerCount);
                    flightBookingRepository.Update(flightBooking);
                }
            }

            if (flightBooking == null || flightBooking.FlightBookingId <= 0)
            {
                flightBooking = new FlightBooking
                {
                    TravelDate = bookingRequest.TravelDate,
                    FlightId = bookingRequest.FlightId,
                    PassengerCount = bookingRequest.PassengerCount
                };

                // Valid booking request, so proceed.
                flightBookingRepository.Add(flightBooking);
            }

            return Ok("Flight Booked.");
        }

        [HttpGet("MyBookings")]
        public IActionResult MyBookings([FromBody] SearchRequest searchRequest)
        {
            return Ok();
        }
    }
}