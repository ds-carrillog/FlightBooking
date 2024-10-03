using Microsoft.AspNetCore.Mvc;
using FlightBookingAPI.Data;
using FlightBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly FlightContext _context;

        // Constructor to inject the database context
        public ReservationsController(FlightContext context)
        {
            _context = context;
        }

        // GET: /Reservations - Get all reservations with associated flight details
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetReservations()
        {
            // Join Reservations with Flights to get reservation and flight details
            var reservations = _context.Reservations
                .Join(
                    _context.Flights,               // Join with Flights table
                    reservation => reservation.FlightId,   // Match on FlightId
                    flight => flight.Id,             // Flight primary key
                    (reservation, flight) => new     // Create anonymous object containing reservation and flight details
                    {
                        reservation.Id,
                        reservation.PassengerName,
                        reservation.ReservationDate,
                        FlightDetails = new           // Flight details as a nested object
                        {
                            flight.Origin,
                            flight.Destination,
                            flight.DepartureDate,
                            flight.ArrivalDate,
                            flight.Airline
                        }
                    }
                ).ToList();                          // Convert to list for return

            return Ok(reservations);                 // Return the reservations along with their associated flight data
        }

        // POST: /Reservations - Create a new reservation
        [HttpPost]
        public ActionResult<Reservation> CreateReservation([FromBody] Reservation reservation)
        {
            var flight = _context.Flights.Find(reservation.FlightId);
            if (flight == null)
            {
                return NotFound("Flight not found.");
            }

            // Validation: Check if the flight's departure date has already passed
            if (flight.DepartureDate < DateTime.Now)
            {
                return BadRequest("Cannot make a reservation for a flight that has already departed.");
            }

            // If validation passes, proceed with creating the reservation
            reservation.ReservationDate = DateTime.Now; // Set reservation date to current time
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetReservations), new { id = reservation.Id }, reservation);
        }
    }
}
