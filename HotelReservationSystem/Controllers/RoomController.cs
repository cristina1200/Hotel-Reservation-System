using HotelReservationSystem.Data;
using HotelReservationSystem.Managers;
using HotelReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomManager roomManager;

        public RoomController(HotelDbContext dbContext)
        {
            roomManager = new RoomManager(dbContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rooms = roomManager.GetAllRooms();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Room());
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                return View(room);
            }

            room.IsAvailable = true;
            roomManager.AddRoom(room);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            roomManager.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}