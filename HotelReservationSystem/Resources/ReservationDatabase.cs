using HotelReservationSystem.Models;

namespace HotelReservationSystem.Resources
{
    public static class ReservationDatabase
    {
        public static List<Reservation> Reservations { get; } = new List<Reservation>();
    }
}