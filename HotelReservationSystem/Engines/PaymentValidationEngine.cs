using HotelReservationSystem.ViewModels;

namespace HotelReservationSystem.Engines
{
    public class PaymentValidationEngine
    {
        public bool ValidatePayment(PaymentViewModel model)
        {
            if (model == null)
            {
                return false;
            }

            if (model.Amount <= 0)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.CardHolderName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.CardNumber))
            {
                return false;
            }

            if (model.CardNumber.Length < 12)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.Cvv))
            {
                return false;
            }

            if (model.Cvv.Length < 3)
            {
                return false;
            }

            return true;
        }
    }
}