using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}