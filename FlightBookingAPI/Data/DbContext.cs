using Microsoft.EntityFrameworkCore;
using FlightBookingAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FlightBookingAPI.Data
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Flights
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Id = 1,
                    Origin = "New York",
                    Destination = "Los Angeles",
                    DepartureDate = DateTime.Now.AddDays(1),
                    ArrivalDate = DateTime.Now.AddDays(1).AddHours(6),
                    Airline = "Delta",
                    Price = 299.99M
                },
                new Flight
                {
                    Id = 2,
                    Origin = "Chicago",
                    Destination = "Miami",
                    DepartureDate = DateTime.Now.AddDays(2),
                    ArrivalDate = DateTime.Now.AddDays(2).AddHours(3),
                    Airline = "United",
                    Price = 199.99M
                },
                new Flight
                {
                    Id = 3,
                    Origin = "New York",
                    Destination = "San Francisco",
                    DepartureDate = DateTime.Now.AddDays(3),
                    ArrivalDate = DateTime.Now.AddDays(3).AddHours(6),
                    Airline = "Delta",
                    Price = 399.99M
                }
            );

            // Seed Reservations
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    FlightId = 1, // Reference the seeded flight ID
                    PassengerName = "John Doe",
                    ReservationDate = DateTime.Now
                },
                new Reservation
                {
                    Id = 2,
                    FlightId = 2,
                    PassengerName = "Jane Smith",
                    ReservationDate = DateTime.Now.AddDays(1)
                },
                new Reservation
                {
                    Id = 3,
                    FlightId = 3,
                    PassengerName = "Alice Johnson",
                    ReservationDate = DateTime.Now.AddDays(2)
                }
            );
        }
    }
}
