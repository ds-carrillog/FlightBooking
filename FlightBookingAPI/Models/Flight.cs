namespace FlightBookingAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Airline { get; set; }
        public decimal Price { get; set; }
    }
}
