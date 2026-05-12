namespace HotelReservationSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public decimal Amount { get; set; }

        public string CardHolderName { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; }

        public bool IsSuccessful { get; set; }
    }
}