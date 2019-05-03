using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomReservationManagement.Models;
using RoomReservationManagement.GeneralClasses;

namespace RoomReservationManagement.Controllers
{
    public class RoomsController : Controller
    {
        public DataOperations dataOps = new DataOperations();

        /// <summary>
		/// returns the room page view, populating the page with room data
		/// </summary>
		/// <returns></returns>
        public ActionResult Index()
        {
            SecurityCheck secCheck = new SecurityCheck();
            if (secCheck.hasFullAccess() || User.IsInRole("RR_Basic"))
            {
                ViewBag.errorMessage = "";
                ViewBag.successValue = false;
                var roomData = dataOps.getAllRooms();
                return View(roomData);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}