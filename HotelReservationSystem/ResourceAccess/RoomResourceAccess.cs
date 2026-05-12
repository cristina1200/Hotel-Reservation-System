using HotelReservationSystem.Data;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.ResourceAccess
{
    public class RoomResourceAccess
    {
        private readonly HotelDbContext dbContext;

        public RoomResourceAccess(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
            SeedRooms();
        }

        private void SeedRooms()
        {
            if (dbContext.Rooms.Any())
            {
                return;
            }

            dbContext.Rooms.AddRange(
                new Room
                {
                    RoomNumber = "101",
                    RoomType = "Single",
                    PricePerNight = 150,
                    IsAvailable = true
                },
                new Room
                {
                    RoomNumber = "102",
                    RoomType = "Single",
                    PricePerNight = 150,
                    IsAvailable = true
                },
                new Room
                {
                    RoomNumber = "201",
                    RoomType = "Double",
                    PricePerNight = 250,
                    IsAvailable = true
                },
                new Room
                {
                    RoomNumber = "202",
                    RoomType = "Double",
                    PricePerNight = 250,
                    IsAvailable = true
                },
                new Room
                {
                    RoomNumber = "301",
                    RoomType = "Apartment",
                    PricePerNight = 400,
                    IsAvailable = true
                }
            );

            dbContext.SaveChanges();
        }

        public List<Room> GetAllRooms()
        {
            return dbContext.Rooms.ToList();
        }

        public List<Room> GetAvailableRooms()
        {
            return dbContext.Rooms
                .Where(room => room.IsAvailable)
                .ToList();
        }

        public List<Room> GetRoomsByType(string roomType)
        {
            return dbContext.Rooms
                .Where(room => room.RoomType.ToLower() == roomType.ToLower()
                               && room.IsAvailable)
                .ToList();
        }

        public Room? GetRoomById(int id)
        {
            return dbContext.Rooms.FirstOrDefault(room => room.Id == id);
        }

        public void AddRoom(Room room)
        {
            dbContext.Rooms.Add(room);
            dbContext.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            var existingRoom = GetRoomById(room.Id);

            if (existingRoom != null)
            {
                existingRoom.RoomNumber = room.RoomNumber;
                existingRoom.RoomType = room.RoomType;
                existingRoom.PricePerNight = room.PricePerNight;
                existingRoom.IsAvailable = room.IsAvailable;

                dbContext.SaveChanges();
            }
        }

        public void DeleteRoom(int id)
        {
            var room = GetRoomById(id);

            if (room != null)
            {
                dbContext.Rooms.Remove(room);
                dbContext.SaveChanges();
            }
        }
    }
}