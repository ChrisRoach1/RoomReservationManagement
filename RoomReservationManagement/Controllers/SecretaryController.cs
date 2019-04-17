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

    }
}