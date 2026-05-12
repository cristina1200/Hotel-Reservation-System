using HotelReservationSystem.Data;
using HotelReservationSystem.Managers;
using HotelReservationSystem.ResourceAccess;
using HotelReservationSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationManager reservationManager;
        private readonly RoomResourceAccess roomResourceAccess;
        private readonly ReservationResourceAccess reservationResourceAccess;

        public ReservationController(HotelDbContext dbContext)
        {
            reservationManager = new ReservationManager(dbContext);
            roomResourceAccess = new RoomResourceAccess(dbContext);
            reservationResourceAccess = new ReservationResourceAccess(dbContext);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ReservationViewModel();
            PopulateReservationViewModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateReservationViewModel(model);
                return View(model);
            }

            var reservation = reservationManager.CreateReservation(model);

            if (reservation == null)
            {
                ViewBag.ErrorMessage = "Reservation could not be created. The selected room may already be reserved for that period.";
                PopulateReservationViewModel(model);
                return View(model);
            }

            return RedirectToAction("Success", new { reservationId = reservation.Id });
        }

        [HttpGet]
        public IActionResult Success(int reservationId)
        {
            var reservation = reservationManager.GetReservationById(reservationId);

            if (reservation == null)
            {
                return RedirectToAction("Create");
            }

            return View(reservation);
        }

        [HttpGet]
        public IActionResult All()
        {
            var reservations = reservationManager.GetAllReservations();
            return View(reservations);
        }

        private void PopulateReservationViewModel(ReservationViewModel model)
        {
            var rooms = roomResourceAccess.GetAllRooms();

            model.Rooms = rooms;
            model.ReservedDatesByRoom = new Dictionary<int, List<string>>();

            foreach (var room in rooms)
            {
                var reservations = reservationResourceAccess.GetReservationsForRoom(room.Id);
                var reservedDates = new List<string>();

                foreach (var reservation in reservations)
                {
                    DateTime currentDate = reservation.CheckInDate.Date;

                    while (currentDate < reservation.CheckOutDate.Date)
                    {
                        reservedDates.Add(currentDate.ToString("yyyy-MM-dd"));
                        currentDate = currentDate.AddDays(1);
                    }
                }

                model.ReservedDatesByRoom[room.Id] = reservedDates;
            }
        }
    }
}