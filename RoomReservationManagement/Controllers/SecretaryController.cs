using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RoomReservationManagement.GeneralClasses;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.Controllers
{
    public class SecretaryController : Controller
    {

        public DataOperations dataOps = new DataOperations();
        public SecurityCheck secCheck = new SecurityCheck();
        public ErrorLogging errorLog = new ErrorLogging();

		/// <summary>
		/// Returns the index view for the secretary
		/// </summary>
		/// <returns></returns>
        public ActionResult Index()
        {
            if (secCheck.hasSecretaryAccess())
            {
                List<res_reservations> resList = dataOps.getAllPendingReservations();
                return View(resList);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }

        }

		/// <summary>
		/// takes in the id of a reservation and rejects it
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public ActionResult reject(int id)
        {
            if (secCheck.hasSecretaryAccess())
            {
                try
                {
					EmailHelper emailHelper = new EmailHelper();
					res_reservations tempData = dataOps.getReservation(id);
					string rejectEmail = "Your reservation request for: " + tempData.Res_Rooms.room_name + " has been rejected, please contact an adminstrator for more details.";
					string toAddr = dataOps.getEmailOnReservation(id);
					emailHelper.sendEmail(toAddr, rejectEmail, "Request Rejected");
                    dataOps.rejectReservation(id);

                    return RedirectToAction("Index", "Secretary");
                }catch(Exception e)
                {
                    errorLog.log_error("Room Reservation Management", "Secretary", "reject", e.Message);
					return View("Error");
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
		/// <summary>
		/// takes in the id of a reservation and approves it
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public ActionResult approve(int id)
        {
            if (secCheck.hasSecretaryAccess())
            {
                try
                {
                    dataOps.approveReservation(id);
                    return RedirectToAction("Index", "Secretary");
                }
                catch (Exception e)
                {
                    errorLog.log_error("Room Reservation Management", "Secretary", "approve", e.Message);
					return View("Error");
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


		public ActionResult approvedRequests()
		{
			if (secCheck.hasSecretaryAccess())
			{
				List<res_reservations> resList = dataOps.getAllAcceptedReservations();
				return View(resList);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public ActionResult cancelReservation(int id)
		{
			if (secCheck.hasSecretaryAccess())
			{
				cancelReservation tempData = new cancelReservation();
				tempData.res_id = id;
				tempData.cancelReason = "";
				ViewBag.successValue = false;
				return View(tempData);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public ActionResult cancelReservation(cancelReservation cancelForm)
		{
			if (secCheck.hasSecretaryAccess())
			{
				try
				{
					EmailHelper emailHelper = new EmailHelper();
					res_reservations tempData = dataOps.getReservation(cancelForm.res_id);
					string rejectEmail = "Your reservation request for: " + tempData.Res_Rooms.room_name + " has been canceled for the following reason(s): " + "\"" + cancelForm.cancelReason + "\"" +  " if you have any questions please contact an adminstrator.";
					string toAddr = dataOps.getEmailOnReservation(cancelForm.res_id);
					emailHelper.sendEmail(toAddr, rejectEmail, "Request Rejected");
					dataOps.rejectReservation(cancelForm.res_id);
					ViewBag.successValue = true;
					return View();
				}
				catch(Exception e)
				{
					errorLog.log_error("Room Reservation Management", "Secretary", "approve", e.Message);
					return View("Error");
				}
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}


	}
}