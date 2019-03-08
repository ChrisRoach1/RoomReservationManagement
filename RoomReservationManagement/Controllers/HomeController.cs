using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomReservationManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Request.IsAuthenticated && User.IsInRole("RR_Admin"))
            {
                Console.WriteLine("hello");
            }
            return View();
        }

    }
}