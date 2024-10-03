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
            // Ensure the flight exists before creating the reservation
            var flight = _context.Flights.Find(reservation.FlightId);
            if (flight == null)
            {
                // Return 404 if the flight does not exist
                return NotFound("Flight not found");
            }

            // Set the reservation date to the current time
            reservation.ReservationDate = DateTime.Now;

            // Add the reservation to the database
            _context.Reservations.Add(reservation);
            _context.SaveChanges();  // Save changes to the database

            // Return the created reservation along with its location
            return CreatedAtAction(nameof(GetReservations), new { id = reservation.Id }, reservation);
        }
    }
}
