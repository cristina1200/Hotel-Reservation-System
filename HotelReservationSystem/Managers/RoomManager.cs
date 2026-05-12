using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using HotelReservationSystem.ResourceAccess;
using HotelReservationSystem.ViewModels;

namespace HotelReservationSystem.Managers
{
    public class RoomManager
    {
        private readonly RoomResourceAccess roomResourceAccess;
        private readonly ReservationResourceAccess reservationResourceAccess;

        public RoomManager(HotelDbContext dbContext)
        {
            roomResourceAccess = new RoomResourceAccess(dbContext);
            reservationResourceAccess = new ReservationResourceAccess(dbContext);
        }

        public List<Room> GetAllRooms()
        {
            return roomResourceAccess.GetAllRooms();
        }

        public List<Room> GetAvailableRooms()
        {
            return roomResourceAccess.GetAvailableRooms();
        }

        public Room? GetRoomById(int id)
        {
            return roomResourceAccess.GetRoomById(id);
        }

        public void AddRoom(Room room)
        {
            roomResourceAccess.AddRoom(room);
        }

        public void DeleteRoom(int id)
        {
            roomResourceAccess.DeleteRoom(id);
        }

        public List<RoomAvailabilityViewModel> GetRoomsWithAvailability()
        {
            var rooms = roomResourceAccess.GetAllRooms();
            var result = new List<RoomAvailabilityViewModel>();

            foreach (var room in rooms)
            {
                var reservations = reservationResourceAccess.GetReservationsForRoom(room.Id);

                var reservedDates = new List<DateTime>();

                foreach (var reservation in reservations)
                {
                    DateTime currentDate = reservation.CheckInDate.Date;

                    while (currentDate < reservation.CheckOutDate.Date)
                    {
                        reservedDates.Add(currentDate);
                        currentDate = currentDate.AddDays(1);
                    }
                }

                var roomAvailability = new RoomAvailabilityViewModel
                {
                    RoomId = room.Id,
                    RoomNumber = room.RoomNumber,
                    RoomType = room.RoomType,
                    PricePerNight = room.PricePerNight,
                    ReservedDates = reservedDates
                };

                result.Add(roomAvailability);
            }

            return result;
        }
    }
}