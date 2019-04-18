using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RoomReservationManagement.Models;
using RoomReservationManagement.GeneralClasses;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

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

		/// <summary>
		/// Returns the manage room view
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Returns the add new room view
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Function to add in a new room into the system, 
		/// then will return back the "addNewRoom" view
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Function will enable a room (swith void_ind to 'n')
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Function will disable a room (switch void_ind to 'y')
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Takes in a room_id and then redirects to the edit
		/// room page with that rooms data
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Will post the room detail changes
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
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

		/// <summary>
		/// returns the manager users view
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// will completely delete a user from the system
		/// THERE IS NO BRINGING IT BACK ONCE ITS GONE
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Will take in a user id and redirect to the password 
		/// change view so the password may be changed for that
		/// specific user account
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult accountPasswordChange(string id)
		{
			if (secCheck.hasAdminAccess())
			{
				string[] roles;
				accountRoleChange tempData = new accountRoleChange();
				
				tempData.userName = dataOps.getUserEmail(id);
				roles = Roles.GetRolesForUser(tempData.userName);
				if(roles != null)
				{
					tempData.oldRole = roles[0];
				}
				tempData.newRole = "";
				ViewBag.successValue = false;
				return View(tempData);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		/// <summary>
		/// Post the data change to change an account password
		/// </summary>
		/// <param name="reset"></param>
		/// <returns></returns>
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

		/// <summary>
		/// takes in a user id and then redirects to the accountRoleChange
		/// page so the users role may be changed
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult accountRoleChange(string id)
		{
			if (secCheck.hasAdminAccess())
			{
				string[] roles;
				accountRoleChange tempData = new accountRoleChange();
				ViewBag.roleList = dataOps.getAllRoles();
				var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
				var roleManager = userManager.GetRoles(id);
				tempData.userName = dataOps.getUserEmail(id);
				ViewBag.successValue = false;
				tempData.userID = id;			
				if (roleManager != null)
				{
					tempData.oldRole = roleManager[0];
				}
				tempData.newRole = "";
				return View(tempData);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		/// <summary>
		/// Post the new user role so it may be updated
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult accountRoleChange(accountRoleChange account)
		{
			if (secCheck.hasAdminAccess())
			{
				if (ModelState.IsValid)
				{
					try
					{
						dataOps.updateUserRole(account);
						ViewBag.successValue = true;
						ViewBag.roleList = dataOps.getAllRoles();
						return View();

					}
					catch (Exception e)
					{
						errorLog.log_error("Room Reservation Management", "Admin", "accountRoleChange", e.Message);
						return View("Error");
					}
				}
				else
				{
					ViewBag.successValue = false;
					return View(account);
				}
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
	}
}