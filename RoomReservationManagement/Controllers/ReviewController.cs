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
using PagedList;

namespace RoomReservationManagement.Controllers
{
    public class ReviewController : Controller
    {

		public class ratings
		{
			public string userView { get; set; }
			public int ratingValue { get; set; }
		}

        public SecurityCheck secCheck = new SecurityCheck();
        public DataOperations dataOps = new DataOperations();
		public ErrorLogging errorLog = new ErrorLogging();


		/// <summary>
		/// returns the view to display all reviews
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		public ActionResult allReviews(int? page)
        {
            if (secCheck.hasManagerAccess())
            {
				int pageSize = 6;
				int pageIndex = 1;
				pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                var revData = dataOps.getAllReviews();
                return View(revData.ToPagedList(pageIndex,pageSize));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

		/// <summary>
		/// returns the view to read review
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public ActionResult reviewDetails(int id)
        {
            if (secCheck.hasManagerAccess())
            {
                var revData = dataOps.getReview(id);
                return View(revData);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

		/// <summary>
		/// returns view to create a new review
		/// </summary>
		/// <returns></returns>
        public ActionResult addReview()
        {
            if (secCheck.hasManagerAccess())
            {
				ViewBag.successValue = false;
				List<ratings> ratingValues = new List<ratings>();
				ratingValues.Add(new ratings { userView = "1 star", ratingValue = 1 });
				ratingValues.Add(new ratings { userView = "2 stars", ratingValue = 2 });
				ratingValues.Add(new ratings { userView = "3 stars", ratingValue = 3 });
				ratingValues.Add(new ratings { userView = "4 stars", ratingValue = 4 });
				ratingValues.Add(new ratings { userView = "5 stars", ratingValue = 5 });
				ViewBag.ratingValues = ratingValues;
				ViewBag.roomList = dataOps.getAllAvailableRooms();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

		/// <summary>
		/// post method to add a new review
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult addReview(res_reviews model)
		{
			if (secCheck.hasManagerAccess())
			{
				try
				{
					
					DateTime currentTime = DateTime.Now;
					model.void_ind = "n";
					model.audit_create_dt = Convert.ToDateTime(currentTime.ToString("yyyy-MM-dd H:mm:ss"));
					dataOps.addReview(model);
					ViewBag.successValue = true;
					List<ratings> ratingValues = new List<ratings>();
					ratingValues.Add(new ratings { userView = "1 star", ratingValue = 1 });
					ratingValues.Add(new ratings { userView = "2 stars", ratingValue = 2 });
					ratingValues.Add(new ratings { userView = "3 stars", ratingValue = 3 });
					ratingValues.Add(new ratings { userView = "4 stars", ratingValue = 4 });
					ratingValues.Add(new ratings { userView = "5 stars", ratingValue = 5 });
					ViewBag.ratingValues = ratingValues;
					ViewBag.roomList = dataOps.getAllAvailableRooms();
					return View();

				}
				catch(Exception e)
				{
					errorLog.log_error("Room Reservation Management", "Review", "addReview", e.Message);
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