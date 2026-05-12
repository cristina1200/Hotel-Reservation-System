using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.ViewModels
{
    public class PaymentViewModel
    {
        public int ReservationId { get; set; }

        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Card holder name is required.")]
        public string CardHolderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card number is required.")]
        [MinLength(12, ErrorMessage = "Card number must have at least 12 digits.")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "CVV is required.")]
        [MinLength(3, ErrorMessage = "CVV must have at least 3 digits.")]
        [MaxLength(4, ErrorMessage = "CVV must have maximum 4 digits.")]
        public string Cvv { get; set; } = string.Empty;
    }
}