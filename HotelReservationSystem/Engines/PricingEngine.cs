using HotelReservationSystem.Models;

namespace HotelReservationSystem.Engines
{
    public class PricingEngine
    {
        public decimal CalculatePrice(Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            int numberOfNights = (checkOutDate - checkInDate).Days;

            if (numberOfNights <= 0)
            {
                return 0;
            }

            return numberOfNights * room.PricePerNight;
        }
    }
}