using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.Controllers
{
    public class RoomsController : Controller
    {
        public DataOperations dataOps = new DataOperations();

        // GET: Rooms
        public ActionResult Index()
        {
            var roomData = dataOps.getAllRooms();
            return View(roomData);
        }
    }
}