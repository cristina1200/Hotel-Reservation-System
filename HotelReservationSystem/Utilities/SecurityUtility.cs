namespace HotelReservationSystem.Utilities
{
    public class SecurityUtility
    {
        public bool ValidateUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            if (input.Contains("<") || input.Contains(">"))
            {
                return false;
            }

            return true;
        }

        public string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 4)
            {
                return "Invalid card number";
            }

            string lastFourDigits = cardNumber.Substring(cardNumber.Length - 4);
            return "**** **** **** " + lastFourDigits;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return email.Contains("@") && email.Contains(".");
        }
    }
}