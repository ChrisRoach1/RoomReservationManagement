using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.GeneralClasses
{
    public class ErrorLogging
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        public void log_error(string app_name, string controller, string method_name,string error_mes)
        {
            DateTime currentTime = DateTime.Now;
            Convert.ToDateTime(currentTime.ToString("yyyy-MM-dd H:mm:ss"));
            error_log log = new error_log {
                app_name = app_name,
                controller = controller,
                method_name = method_name,
                error_mes = error_mes,
                audit_create_dt = Convert.ToDateTime(currentTime.ToString("yyyy-MM-dd H:mm:ss"))
            };
            db.Error_Logs.Add(log);
            db.SaveChanges();
        }
    }
}