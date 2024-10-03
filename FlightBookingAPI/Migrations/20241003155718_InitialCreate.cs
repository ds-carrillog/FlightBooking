using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Origin = table.Column<string>(type: "TEXT", nullable: false),
                    Destination = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Airline = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    PassengerName = table.Column<string>(type: "TEXT", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Airline", "ArrivalDate", "DepartureDate", "Destination", "Origin", "Price" },
                values: new object[,]
                {
                    { 1, "Delta", new DateTime(2024, 10, 4, 16, 57, 14, 292, DateTimeKind.Local).AddTicks(9507), new DateTime(2024, 10, 4, 10, 57, 14, 292, DateTimeKind.Local).AddTicks(9481), "Los Angeles", "New York", 299.99m },
                    { 2, "United", new DateTime(2024, 10, 5, 13, 57, 14, 292, DateTimeKind.Local).AddTicks(9514), new DateTime(2024, 10, 5, 10, 57, 14, 292, DateTimeKind.Local).AddTicks(9513), "Miami", "Chicago", 199.99m },
                    { 3, "Delta", new DateTime(2024, 10, 6, 16, 57, 14, 292, DateTimeKind.Local).AddTicks(9516), new DateTime(2024, 10, 6, 10, 57, 14, 292, DateTimeKind.Local).AddTicks(9516), "San Francisco", "New York", 399.99m }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "FlightId", "PassengerName", "ReservationDate" },
                values: new object[,]
                {
                    { 1, 1, "John Doe", new DateTime(2024, 10, 3, 10, 57, 14, 292, DateTimeKind.Local).AddTicks(9636) },
                    { 2, 2, "Jane Smith", new DateTime(2024, 10, 4, 10, 57, 14, 292, DateTimeKind.Local).AddTicks(9638) },
                    { 3, 3, "Alice Johnson", new DateTime(2024, 10, 5, 10, 57, 14, 292, DateTimeKind.Local).AddTicks(9639) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
