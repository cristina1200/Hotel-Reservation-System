using HotelReservationSystem.Data;
using HotelReservationSystem.Managers;
using HotelReservationSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentManager paymentManager;

        public PaymentController(HotelDbContext dbContext)
        {
            paymentManager = new PaymentManager(dbContext);
        }

        [HttpGet]
        public IActionResult Pay(int reservationId, decimal amount)
        {
            var model = new PaymentViewModel
            {
                ReservationId = reservationId,
                Amount = amount
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Pay(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var paymentResult = paymentManager.ProcessPayment(model);

            if (!paymentResult)
            {
                ViewBag.ErrorMessage = "Payment could not be processed.";
                return View(model);
            }

            TempData["SuccessMessage"] = "Payment processed successfully. A payment notification was generated.";

            return RedirectToAction("Success", "Reservation", new { reservationId = model.ReservationId });
        }
    }
}