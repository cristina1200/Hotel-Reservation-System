namespace HotelReservationSystem.ViewModels
{
    public class RoomAvailabilityViewModel
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }

        public List<DateTime> ReservedDates { get; set; } = new List<DateTime>();
    }
}