namespace HotelReservationSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = new Customer();

        public int RoomId { get; set; }

        public Room Room { get; set; } = new Room();

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsConfirmed { get; set; }
    }
}