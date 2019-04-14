using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomReservationManagement.Models;
using RoomReservationManagement.GeneralClasses;

namespace RoomReservationManagement.Controllers
{
    public class AdminController : Controller
    {

        public DataOperations dataOps = new DataOperations();
        public SecurityCheck secCheck = new SecurityCheck();
        public ErrorLogging errorLog = new ErrorLogging();
        
        public class indicatorItem
        {
            public string display { get; set; }
            public string sourceValue { get; set; }
        }

        public ActionResult ManageRooms()
        {
            if (secCheck.hasAdminAccess())
            {
                List<res_rooms> allRooms = dataOps.getAllRooms();
                return View(allRooms);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddNewRoom()
        {
            if (secCheck.hasAdminAccess())
            {
                List<indicatorItem> indicatorValues = new List<indicatorItem>();
                indicatorValues.Add(new indicatorItem { display = "Yes", sourceValue = "y" });
                indicatorValues.Add(new indicatorItem { display = "No", sourceValue = "n" });
                ViewBag.indicators = indicatorValues;
                ViewBag.successValue = false;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddNewRoom(res_rooms room)
        {

            if (secCheck.hasAdminAccess())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        room.void_ind = "n";
                        dataOps.addRoom(room);
                        ViewBag.successValue = true;
                        List<indicatorItem> indicatorValues = new List<indicatorItem>();
                        indicatorValues.Add(new indicatorItem { display = "Yes", sourceValue = "y" });
                        indicatorValues.Add(new indicatorItem { display = "No", sourceValue = "n" });
                        ViewBag.indicators = indicatorValues;
                        return View();

                    }
                    catch (Exception e)
                    {
                        errorLog.log_error("Room Reservation Management", "Admin", "AddNewRoom", e.Message);
                        return RedirectToAction("Error", "Home");
                    }

                }
                else
                {
                    ViewBag.successValue = false;
                    List<indicatorItem> indicatorValues = new List<indicatorItem>();
                    indicatorValues.Add(new indicatorItem { display = "Yes", sourceValue = "y" });
                    indicatorValues.Add(new indicatorItem { display = "No", sourceValue = "n" });
                    ViewBag.indicators = indicatorValues;
                    return View(room);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EnableRoom(int id)
        {
            if (secCheck.hasAdminAccess())
            {
                try
                {
                    dataOps.enableRoom(id);

                    return RedirectToAction("ManageRooms");
                }
                catch (Exception e)
                {
                    errorLog.log_error("Room Reservation Management", "Admin", "EnableRoom", e.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DisableRoom(int id)
        {
            if (secCheck.hasAdminAccess())
            {
                try
                {
                    dataOps.disableRoom(id);
                    return RedirectToAction("ManageRooms");
                }
                catch (Exception e)
                {
                    errorLog.log_error("Room Reservation Management", "Admin", "DisableRoom", e.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditRoom(int id)
        {
            if (secCheck.hasAdminAccess())
            {
                res_rooms room = dataOps.getRoom(id);
                ViewBag.successValue = false;
                List<indicatorItem> indicatorValues = new List<indicatorItem>();
                indicatorValues.Add(new indicatorItem { display = "Yes", sourceValue = "y" });
                indicatorValues.Add(new indicatorItem { display = "No", sourceValue = "n" });
                ViewBag.indicators = indicatorValues;
                return View(room);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult EditRoom(res_rooms room)
        {
            if (secCheck.hasAdminAccess())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        dataOps.updateRoom(room);
                        ViewBag.successValue = true;
                        List<indicatorItem> indicatorValues = new List<indicatorItem>();
                        indicatorValues.Add(new indicatorItem { display = "Yes", sourceValue = "y" });
                        indicatorValues.Add(new indicatorItem { display = "No", sourceValue = "n" });
                        ViewBag.indicators = indicatorValues;
                        return View();

                    }
                    catch (Exception e)
                    {
                        errorLog.log_error("Room Reservation Management", "Admin", "EditRoom", e.Message);
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    ViewBag.successValue = false;
                    List<indicatorItem> indicatorValues = new List<indicatorItem>();
                    indicatorValues.Add(new indicatorItem { display = "Yes", sourceValue = "y" });
                    indicatorValues.Add(new indicatorItem { display = "No", sourceValue = "n" });
                    ViewBag.indicators = indicatorValues;
                    return View(room);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}