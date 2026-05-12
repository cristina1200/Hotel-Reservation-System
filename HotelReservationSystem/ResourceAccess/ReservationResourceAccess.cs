using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.ResourceAccess
{
    public class ReservationResourceAccess
    {
        private readonly HotelDbContext dbContext;

        public ReservationResourceAccess(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetNextId()
        {
            if (!dbContext.Reservations.Any())
            {
                return 1;
            }

            return dbContext.Reservations.Max(reservation => reservation.Id) + 1;
        }

        public void AddReservation(Reservation reservation)
        {
            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();
        }

        public List<Reservation> GetReservationsForRoom(int roomId)
        {
            return dbContext.Reservations
                .Where(r => r.RoomId == roomId)
                .ToList();
        }
        public Reservation? GetReservationById(int id)
        {
            return dbContext.Reservations
                .Include(reservation => reservation.Customer)
                .Include(reservation => reservation.Room)
                .FirstOrDefault(reservation => reservation.Id == id);
        }

        public List<Reservation> GetAllReservations()
        {
            return dbContext.Reservations
                .Include(reservation => reservation.Customer)
                .Include(reservation => reservation.Room)
                .ToList();
        }

        public List<Reservation> GetReservationsByRoomId(int roomId)
        {
            return dbContext.Reservations
                .Include(reservation => reservation.Customer)
                .Include(reservation => reservation.Room)
                .Where(reservation => reservation.RoomId == roomId)
                .ToList();
        }
    }
}