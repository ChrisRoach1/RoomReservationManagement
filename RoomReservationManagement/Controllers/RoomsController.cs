﻿using System;
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

        // GET: Rooms
        public ActionResult Index()
        {
            SecurityCheck secCheck = new SecurityCheck();
            if (secCheck.hasFullAccess())
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