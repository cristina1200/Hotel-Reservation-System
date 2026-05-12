using HotelReservationSystem.Data;
using HotelReservationSystem.Engines;
using HotelReservationSystem.Models;
using HotelReservationSystem.ResourceAccess;
using HotelReservationSystem.Utilities;
using HotelReservationSystem.ViewModels;

namespace HotelReservationSystem.Managers
{
    public class ReservationManager
    {
        private readonly AvailabilityEngine availabilityEngine;
        private readonly PricingEngine pricingEngine;
        private readonly CustomerResourceAccess customerResourceAccess;
        private readonly ReservationResourceAccess reservationResourceAccess;
        private readonly RoomResourceAccess roomResourceAccess;
        private readonly NotificationManager notificationManager;
        private readonly SecurityUtility securityUtility;

        public ReservationManager(HotelDbContext dbContext)
        {
            availabilityEngine = new AvailabilityEngine(dbContext);
            pricingEngine = new PricingEngine();
            customerResourceAccess = new CustomerResourceAccess(dbContext);
            reservationResourceAccess = new ReservationResourceAccess(dbContext);
            roomResourceAccess = new RoomResourceAccess(dbContext);
            notificationManager = new NotificationManager();
            securityUtility = new SecurityUtility();
        }

        public Reservation? CreateReservation(ReservationViewModel model)
        {
            //validare
            if (!securityUtility.ValidateUserInput(model.CustomerName) ||
                !securityUtility.IsValidEmail(model.Email) ||
                !securityUtility.ValidateUserInput(model.PhoneNumber))
            {
                return null;
            }

            //verif disponibilitate
            if (model.CheckOutDate <= model.CheckInDate)
            {
                return null;
            }

            var selectedRoom = roomResourceAccess.GetRoomById(model.RoomId);

            if (selectedRoom == null)
            {
                return null;
            }

            bool isAvailable = availabilityEngine.IsRoomAvailable(
                selectedRoom.Id,
                model.CheckInDate,
                model.CheckOutDate
            );

            if (!isAvailable)
            {
                return null;
            }

            //salvare client
            var customer = new Customer
            {
                FullName = model.CustomerName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            customerResourceAccess.AddCustomer(customer);

            //calcul pret
            var totalPrice = pricingEngine.CalculatePrice(
                selectedRoom,
                model.CheckInDate,
                model.CheckOutDate
            );

            //salvare rezervare
            var reservation = new Reservation
            {
                CustomerId = customer.Id,
                Customer = customer,
                RoomId = selectedRoom.Id,
                Room = selectedRoom,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                TotalPrice = totalPrice,
                IsConfirmed = true
            };

            reservationResourceAccess.AddReservation(reservation);
            //trimitere notificare
            notificationManager.SendReservationConfirmation(reservation);

            return reservation;
        }

        public Reservation? GetReservationById(int id)
        {
            return reservationResourceAccess.GetReservationById(id);
        }

        public List<Reservation> GetAllReservations()
        {
            return reservationResourceAccess.GetAllReservations();
        }
    }
}