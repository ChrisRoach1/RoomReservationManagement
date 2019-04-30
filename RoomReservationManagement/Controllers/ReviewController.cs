using System;
using System.Collections.Generic;
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
    public class ReviewController : Controller
    {
        SecurityCheck secCheck = new SecurityCheck();
        public DataOperations dataOps = new DataOperations();


        //GET: Reviews
        public ActionResult Index()
        {
            if (secCheck.hasFullAccess())
            {
                ViewBag.errorMessage = "";
                ViewBag.successValue = false;
                var revData = dataOps.getAllReviews();
                return View(revData);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Details()
        {
            if (secCheck.hasFullAccess())
            {
                ViewBag.errorMessage = "";
                ViewBag.successValue = false;
                var revData = dataOps.getAllReviews();
                return View(revData);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult NewTest()
        {
            if (secCheck.hasFullAccess())
            {
                ViewBag.errorMessage = "";
                ViewBag.successValue = false;
                var revData = dataOps.getAllReviews();
                return View(revData);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult NewReview()
        {
            if (secCheck.hasFullAccess())
            {
                ViewBag.errorMessage = "";
                ViewBag.successValue = false;
                var revData = dataOps.getAllReviews();
                return View(revData);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}