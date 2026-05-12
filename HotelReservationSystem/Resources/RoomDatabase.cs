using HotelReservationSystem.Models;

namespace HotelReservationSystem.Resources
{
    public static class RoomDatabase
    {
        public static List<Room> Rooms { get; } = new List<Room>
        {
            new Room
            {
                Id = 1,
                RoomNumber = "101",
                RoomType = "Single",
                PricePerNight = 150,
                IsAvailable = true
            },
            new Room
            {
                Id = 2,
                RoomNumber = "102",
                RoomType = "Single",
                PricePerNight = 150,
                IsAvailable = true
            },
            new Room
            {
                Id = 3,
                RoomNumber = "201",
                RoomType = "Double",
                PricePerNight = 250,
                IsAvailable = true
            },
            new Room
            {
                Id = 4,
                RoomNumber = "202",
                RoomType = "Double",
                PricePerNight = 250,
                IsAvailable = true
            },
            new Room
            {
                Id = 5,
                RoomNumber = "301",
                RoomType = "Apartment",
                PricePerNight = 400,
                IsAvailable = true
            }
        };
    }
}