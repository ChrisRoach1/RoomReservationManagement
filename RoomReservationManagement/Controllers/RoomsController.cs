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
        public ApplicationDbContext databaseConnection = new ApplicationDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            var roomData = databaseConnection.Res_Rooms;
            return View(roomData);
        }
    }
}