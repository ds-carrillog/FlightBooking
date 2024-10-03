using Microsoft.AspNetCore.Mvc;
using FlightBookingAPI.Data;
using FlightBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly FlightContext _context;

        // Constructor to inject the database context
        public FlightsController(FlightContext context)
        {
            _context = context;
        }

        // GET: /Flight - Fetch all available flights
        [HttpGet]
        public ActionResult<IEnumerable<Flight>> GetFlights()
        {
            // Retrieve all flights from the database and return them
            return Ok(_context.Flights.ToList());
        }

        // GET: /Flight/{id} - Fetch a specific flight by its Id
        [HttpGet("{id}")]
        public ActionResult<Flight> GetFlight(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight == null)
            {
                // Return 404 if the flight doesn't exist
                return NotFound();
            }
            return Ok(flight);
        }

        // POST: /Flight - Create a new flight entry
        [HttpPost]
        public ActionResult<Flight> CreateFlight([FromBody] Flight flight)
        {
            _context.Flights.Add(flight); // Add the new flight to the database
            _context.SaveChanges(); // Persist changes

            // Return the newly created flight with its assigned Id
            return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, flight);
        }

        // PUT: /Flight/{id} - Update an existing flight by its Id
        [HttpPut("{id}")]
        public ActionResult UpdateFlight(int id, [FromBody] Flight updatedFlight)
        {
            if (id != updatedFlight.Id)
            {
                // Return 400 if the flight Id doesn't match the payload
                return BadRequest();
            }

            var flight = _context.Flights.Find(id);
            if (flight == null)
            {
                // Return 404 if the flight is not found
                return NotFound();
            }

            // Update flight details with the provided data
            flight.Origin = updatedFlight.Origin;
            flight.Destination = updatedFlight.Destination;
            flight.DepartureDate = updatedFlight.DepartureDate;
            flight.ArrivalDate = updatedFlight.ArrivalDate;
            flight.Airline = updatedFlight.Airline;
            flight.Price = updatedFlight.Price;

            _context.Entry(flight).State = EntityState.Modified; // Mark entity as modified
            _context.SaveChanges(); // Save the changes to the database

            return NoContent(); // Return 204 after a successful update
        }

        // DELETE: /Flight/{id} - Remove a flight by its Id
        [HttpDelete("{id}")]
        public ActionResult DeleteFlight(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight == null)
            {
                // Return 404 if the flight to delete is not found
                return NotFound();
            }

            // Remove the flight from the database
            _context.Flights.Remove(flight);
            _context.SaveChanges(); // Save the changes

            return NoContent(); // Return 204 after successful deletion
        }

        // GET: /Flight/Search - Search for flights based on origin, destination, and date range
        [HttpGet("Search")]
        public ActionResult<IEnumerable<Flight>> SearchFlights(
            [FromQuery] string? origin = null,           // Optional origin parameter
            [FromQuery] string? destination = null,      // Optional destination parameter
            [FromQuery] DateTime? startDate = null,      // Optional start date
            [FromQuery] DateTime? endDate = null)        // Optional end date
        {
            // Start with the complete list of flights, then apply filters
            var flights = _context.Flights.AsQueryable();

            // Filter by origin if provided
            if (!string.IsNullOrEmpty(origin))
            {
                flights = flights.Where(f => f.Origin.ToLower() == origin.ToLower());
            }

            // Filter by destination if provided
            if (!string.IsNullOrEmpty(destination))
            {
                flights = flights.Where(f => f.Destination.ToLower() == destination.ToLower());
            }

            // Filter by departure date range if provided
            if (startDate.HasValue)
            {
                flights = flights.Where(f => f.DepartureDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                var endDateWithTime = endDate.Value.AddDays(1).AddTicks(-1); // Include full day for endDate
                flights = flights.Where(f => f.DepartureDate <= endDateWithTime);
            }

            return Ok(flights.ToList()); // Return the filtered list of flights
        }

        // GET: /Flight/Statistics/AirlinesCount - Retrieve the count of distinct airlines
        [HttpGet("Statistics/AirlinesCount")]
        public ActionResult<int> GetAirlinesCount()
        {
            // Count the distinct airlines across all flights
            var airlineCount = _context.Flights.Select(f => f.Airline).Distinct().Count();
            return Ok(airlineCount);
        }

        // GET: /Flight/Statistics/TopAirlines - Retrieve the top 5 airlines by reservation count
        [HttpGet("Statistics/TopAirlines")]
        public ActionResult<IEnumerable<object>> GetTopAirlines()
        {
            // Join Reservations and Flights to count the number of reservations per airline
            var topAirlines = _context.Reservations
                .Join(
                    _context.Flights,               // Join Reservations with Flights on FlightId
                    reservation => reservation.FlightId,
                    flight => flight.Id,
                    (reservation, flight) => new
                    {
                        flight.Airline,
                        ReservationId = reservation.Id
                    }
                )
                .GroupBy(x => x.Airline)            // Group by airline
                .Select(g => new
                {
                    Airline = g.Key,                // Airline name
                    ReservationsCount = g.Count()   // Count of reservations per airline
                })
                .OrderByDescending(g => g.ReservationsCount) // Sort by reservation count in descending order
                .Take(5)                            // Limit to the top 5 airlines
                .ToList();

            return Ok(topAirlines);                 // Return the top airlines
        }
    }
}
