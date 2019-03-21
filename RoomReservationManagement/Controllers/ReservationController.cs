using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomReservationManagement.GeneralClasses;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.Controllers
{
    public class ReservationController : Controller
    {

        public DataOperations dataOps = new DataOperations();

        // GET: Reservation
        public ActionResult Index()
        {
            ViewBag.roomList = dataOps.getAllRooms();

            return View();
        }
    }
}