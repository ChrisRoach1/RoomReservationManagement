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
        public DataOperations dataOps = new DataOperations();


        //GET: Reviews
        public ActionResult Index()
        {
            var revData = dataOps.getAllReviews();
            return View(revData);
        }

    }
}