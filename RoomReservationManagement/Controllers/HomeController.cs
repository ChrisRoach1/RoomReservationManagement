﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomReservationManagement.Models;
using RoomReservationManagement.GeneralClasses;

namespace RoomReservationManagement.Controllers
{

    public class events
    {
        public string start;
        public string end;
        public string title;
        public string backgroundColor;
    }

    public class eventList
    {
       public List<events> eList = new List<events>();
      
    }

    public class HomeController : Controller
    {

        public ApplicationDbContext databaseConnection = new ApplicationDbContext();


        public ActionResult Index()
        {

            eventList eventList = new eventList();

            eventList.eList = getEventDates();
            ViewBag.listOfEvents = eventList.eList;
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

		public ActionResult RequestRegister()
		{
			ViewBag.successValue = false;
			return View();
			
		}

		[HttpPost]
		public ActionResult RequestRegister(RequestRegister request)
		{
			DataOperations dataOps = new DataOperations();
			EmailHelper emailHelper = new EmailHelper();

			string AdminEmail = dataOps.getAdminEmail();
			string body;
			

			if (ModelState.IsValid)
			{
				body = request.Name + ", email address: " + request.email + " has requested an account for the following reason(s): " + request.reqReason;
				emailHelper.sendEmail(AdminEmail, body, "Room Reservation Request");
				ViewBag.successValue = true;
				return View();
			}
			else
			{
				return View(request);
				ViewBag.successValue = false;
			}
		}

		public List<events> getEventDates()
        { 
            DateTime today = DateTime.Now.Date;
            eventList eventList = new eventList();
            var futureEvents = databaseConnection.Res_Reservations.Where(r => r.res_start >= today && r.void_ind == "n" && r.reject_ind == "n").ToList();

            foreach(var eventDate in futureEvents)
            {
                string start = eventDate.res_start.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff''");
                string end = eventDate.res_end.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff''");
                string backgroundColor = (eventDate.approved_ind == "y" ? "lime" : "orange");
                eventList.eList.Add(new events
                {
                    start = start,
                    end = end,
                    title = eventDate.Res_Rooms.room_name,
                    backgroundColor = backgroundColor
                });
            }

            return eventList.eList;
        }

    }
}