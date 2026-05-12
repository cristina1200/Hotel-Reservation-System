using HotelReservationSystem.Models;

namespace HotelReservationSystem.Engines
{
    public class NotificationEngine
    {
        public string GenerateReservationConfirmationMessage(Reservation reservation)
        {
            return "Reservation confirmed for " + reservation.Customer.FullName +
                   ". Room: " + reservation.Room.RoomNumber +
                   ", Check-in: " + reservation.CheckInDate.ToShortDateString() +
                   ", Check-out: " + reservation.CheckOutDate.ToShortDateString() +
                   ", Total price: " + reservation.TotalPrice + " RON.";
        }

        public string GeneratePaymentConfirmationMessage(Payment payment)
        {
            return "Payment confirmed. Payment ID: " + payment.Id +
                   ", Reservation ID: " + payment.ReservationId +
                   ", Amount: " + payment.Amount + " RON.";
        }
    }
}