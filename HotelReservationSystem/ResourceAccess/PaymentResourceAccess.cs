using HotelReservationSystem.Data;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.ResourceAccess
{
    public class PaymentResourceAccess
    {
        private readonly HotelDbContext dbContext;

        public PaymentResourceAccess(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetNextId()
        {
            if (!dbContext.Payments.Any())
            {
                return 1;
            }

            return dbContext.Payments.Max(payment => payment.Id) + 1;
        }

        public void AddPayment(Payment payment)
        {
            dbContext.Payments.Add(payment);
            dbContext.SaveChanges();
        }

        public List<Payment> GetAllPayments()
        {
            return dbContext.Payments.ToList();
        }

        public Payment? GetPaymentByReservationId(int reservationId)
        {
            return dbContext.Payments
                .FirstOrDefault(payment => payment.ReservationId == reservationId);
        }
    }
}