using HotelReservationSystem.Engines;
using HotelReservationSystem.Models;
using HotelReservationSystem.Utilities;

namespace HotelReservationSystem.Managers
{
    public class NotificationManager
    {
        private readonly NotificationEngine notificationEngine;
        private readonly NotificationUtility notificationUtility;

        public NotificationManager()
        {
            notificationEngine = new NotificationEngine();
            notificationUtility = new NotificationUtility();
        }

        public void SendReservationConfirmation(Reservation reservation)
        {
            string message = notificationEngine.GenerateReservationConfirmationMessage(reservation);
            notificationUtility.SendNotification(reservation.Customer.Email, message);
        }

        public void SendPaymentConfirmation(Payment payment)
        {
            string message = notificationEngine.GeneratePaymentConfirmationMessage(payment);
            notificationUtility.SendNotification("customer@email.com", message);
        }
    }
}