using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using HotelReservationSystem.ResourceAccess;

namespace HotelReservationSystem.Engines
{
    public class AvailabilityEngine
    {
        private readonly RoomResourceAccess roomResourceAccess;
        private readonly ReservationResourceAccess reservationResourceAccess;

        public AvailabilityEngine(HotelDbContext dbContext)
        {
            roomResourceAccess = new RoomResourceAccess(dbContext);
            reservationResourceAccess = new ReservationResourceAccess(dbContext);
        }

        public Room? FindAvailableRoom(string roomType, DateTime checkInDate, DateTime checkOutDate)
        {
            var rooms = roomResourceAccess.GetRoomsByType(roomType);

            foreach (var room in rooms)
            {
                if (IsRoomAvailable(room.Id, checkInDate, checkOutDate))
                {
                    return room;
                }
            }

            return null;
        }

        //disponibilitate
        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var reservations = reservationResourceAccess.GetReservationsByRoomId(roomId);

            foreach (var reservation in reservations)
            {
                bool overlaps = checkInDate < reservation.CheckOutDate &&
                                checkOutDate > reservation.CheckInDate;

                if (overlaps)
                {
                    return false;
                }
            }

            return true;
        }
    }
}