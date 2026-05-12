using HotelReservationSystem.Data;
using HotelReservationSystem.Engines;
using HotelReservationSystem.Models;
using HotelReservationSystem.ResourceAccess;
using HotelReservationSystem.Utilities;
using HotelReservationSystem.ViewModels;

namespace HotelReservationSystem.Managers
{
    public class PaymentManager
    {
        private readonly PaymentValidationEngine paymentValidationEngine;
        private readonly PaymentResourceAccess paymentResourceAccess;
        private readonly NotificationManager notificationManager;
        private readonly SecurityUtility securityUtility;
        private readonly LoggingUtility loggingUtility;

        public PaymentManager(HotelDbContext dbContext)
        {
            paymentValidationEngine = new PaymentValidationEngine();
            paymentResourceAccess = new PaymentResourceAccess(dbContext);
            notificationManager = new NotificationManager();
            securityUtility = new SecurityUtility();
            loggingUtility = new LoggingUtility();
        }

        public bool ProcessPayment(PaymentViewModel model)
        {
            bool isValid = paymentValidationEngine.ValidatePayment(model);

            if (!isValid)
            {
                loggingUtility.Log("Payment validation failed for reservation ID: " + model.ReservationId);
                return false;
            }

            string maskedCardNumber = securityUtility.MaskCardNumber(model.CardNumber);

            var payment = new Payment
            {
                ReservationId = model.ReservationId,
                Amount = model.Amount,
                CardHolderName = model.CardHolderName,
                PaymentDate = DateTime.Now,
                IsSuccessful = true
            };

            paymentResourceAccess.AddPayment(payment);

            loggingUtility.Log("Payment processed successfully for reservation ID: " +
                               model.ReservationId +
                               " using card " +
                               maskedCardNumber);

            notificationManager.SendPaymentConfirmation(payment);

            return true;
        }

        public List<Payment> GetAllPayments()
        {
            return paymentResourceAccess.GetAllPayments();
        }

        public Payment? GetPaymentByReservationId(int reservationId)
        {
            return paymentResourceAccess.GetPaymentByReservationId(reservationId);
        }
    }
}