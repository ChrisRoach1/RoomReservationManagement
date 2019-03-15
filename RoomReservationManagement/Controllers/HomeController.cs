using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomReservationManagement.Controllers
{

    public class events
    {
        public string start;
        public string end;
        public string title;
    }

    public class eventList
    {
       public List<events> eList = new List<events>();
      
    }

    public class HomeController : Controller
    {



        public ActionResult Index()
        {
            if(Request.IsAuthenticated && User.IsInRole("RR_Admin"))
            {
                Console.WriteLine("hello");
            }

            eventList lis = new eventList();


            events ev1 = new events();
            ev1.start = "2019-03-12T11:00:00";
            ev1.end = "2019-03-12T14:00:00";
            ev1.title = "test1";
            events ev2 = new events();
            ev2.start = "2019-03-15T12:35:00";
            ev2.end = "2019-03-15T13:35:00";
            ev2.title = "test2";
            events ev3 = new events();
            ev3.start = "2019-03-23T11:07:56.000";
            ev3.end = "2019-03-23T12:17:56.000";
            ev3.title = "test3";
            events ev4 = new events();
            ev4.start = "2019-03-23T11:07:56.000";
            ev4.end = "2019-03-23T12:17:56.000";
            ev4.title = "test4";
            events ev5 = new events();
            ev5.start = "2019-03-23T11:07:56.000";
            ev5.end = "2019-03-23T12:17:56.000";
            ev5.title = "test5";
            events ev6 = new events();
            ev6.start = "2019-03-23T11:07:56.000";
            ev6.end = "2019-03-23T12:17:56.000";
            ev6.title = "test6";
            lis.eList.Add(ev1);
            lis.eList.Add(ev2);
            lis.eList.Add(ev3);
            lis.eList.Add(ev4);
            lis.eList.Add(ev5);
            lis.eList.Add(ev6);

            events[] allEvents = new events[6];
            allEvents[0] = ev1;
            allEvents[1] = ev2;
            allEvents[2] = ev3;
            allEvents[3] = ev4;
            allEvents[4] = ev5;
            allEvents[5] = ev6;

            
            ViewBag.listOfEvents = allEvents;
            return View();
        }

    }
}