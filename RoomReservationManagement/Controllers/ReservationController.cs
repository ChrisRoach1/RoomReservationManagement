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
    public class ReservationController : Controller
    {

        public DataOperations dataOps = new DataOperations();
        public ErrorLogging errorLog = new ErrorLogging();
        
        
        public ActionResult makeReservation()
        {
            SecurityCheck secCheck = new SecurityCheck();
            if(secCheck.isVerified())
            {
                ViewBag.roomList = dataOps.getAllRooms();
                ViewBag.errorMessage = "";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /*
         * Need to add in the email functionality at a later point*****
         */
        [HttpPost]
        public ActionResult makeReservation(res_reservations reservation)
        {
            DateTimeHelper dtHelper = new DateTimeHelper();
            VerificationCodeGenerator codeGen = new VerificationCodeGenerator();
            SecurityCheck secCheck = new SecurityCheck();

            DateTime startTime = dtHelper.convertStringToDatetime(dtHelper.convertStampToDateString(reservation.res_dt), dtHelper.convertStampToTimeString(reservation.res_start));
            DateTime endTime = dtHelper.convertStringToDatetime(dtHelper.convertStampToDateString(reservation.res_dt), dtHelper.convertStampToTimeString(reservation.res_end));
            List<res_reservations> roomsInTimeRange = dataOps.getReservationWithStartTime(startTime, endTime, reservation.room_id);

            if(!String.IsNullOrEmpty(reservation.cat_order))
            {
                reservation.cat_ind = "y";
                reservation.ver_code = codeGen.getVerificationCode();
            }
            else
            {
                reservation.cat_ind = "n";
            }
      
            if (secCheck.isVerified())
            {
                try
                {
                    if (roomsInTimeRange.Count == 0)
                    {
                        reservation.void_ind = "n";
                        reservation.user_id = User.Identity.GetUserId();
                        reservation.res_start = startTime;
                        reservation.res_end = endTime;
                        reservation.void_ind = "n";
                        reservation.approved_ind = "n";
                        reservation.reject_ind = "n";
                        reservation.pending_ind = "y";

                        dataOps.addReservation(reservation);
                        ViewBag.errorMessage = "";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.errorMessage = "This time is already reserved!";
                        ViewBag.roomList = dataOps.getAllRooms();
                        return View(reservation);
                    }
                }catch(Exception e)
                {
                    errorLog.log_error("Room Reservation Management", "Reservation", "makeReservation", e.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}