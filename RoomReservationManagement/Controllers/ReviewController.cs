using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.Controllers
{
    public class ReviewController : Controller
    {
        public DataOperations dataOps = new DataOperations();

        // GET: Rooms
        public ActionResult Index()
        {
            var roomData = dataOps.getAllRooms();
            return View(roomData);
        }

        public ActionResult Details()
        {
            var roomData = dataOps.getAllRooms();
            return View(roomData);
        }

        public ActionResult TestSandbox()
        {
            var roomData = dataOps.getAllRooms();
            return View(roomData);
        }
    }
}