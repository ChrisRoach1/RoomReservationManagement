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
                        return View("Error");
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
					return View("Error");
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
					return View("Error");
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
						return View("Error");
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


		public ActionResult ManageUsers()
		{

			if (secCheck.hasAdminAccess())
			{
				ViewBag.successValue = false;
				List<ApplicationUser> userList = dataOps.getAllUsers();
				return View(userList);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		
		public ActionResult accountDelete(string id)
		{
			if (secCheck.hasAdminAccess())
			{
				try
				{
					dataOps.deleteUserAccount(id);
					ViewBag.successValue = true;
					List<ApplicationUser> userList = dataOps.getAllUsers();
					return RedirectToAction("ManageUsers");

				}
				catch(Exception e)
				{
					errorLog.log_error("Room Reservation Management", "Admin", "accountDelete", e.Message);
					return View("Error");
				}
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		
		public ActionResult accountPasswordChange(string id)
		{
			if (secCheck.hasAdminAccess())
			{
				PasswordReset tempData = new PasswordReset();
				tempData.userID = id;
				tempData.Email = dataOps.getUserEmail(id);
				ViewBag.successValue = false;
				return View(tempData);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public ActionResult accountPasswordChange(PasswordReset reset)
		{
			if (secCheck.hasAdminAccess())
			{
				if (ModelState.IsValid)
				{
					try
					{
						dataOps.updateUserPassword(reset);
						ViewBag.successValue = true;
						return View();

					}
					catch(Exception e)
					{
						errorLog.log_error("Room Reservation Management", "Admin", "accountPasswordChange", e.Message);
						return View("Error");
					}
				}
				else
				{
					ViewBag.successValue = false;
					return View(reset);
				}
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
	}
}