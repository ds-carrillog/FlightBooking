namespace FlightBookingAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public string PassengerName { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
