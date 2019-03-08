using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.GeneralClasses
{
    public class DateTimeHelper
    {

        /*
         * This class is mainly used to abstract string to datetime 
         * and datetime to string conversion, making it easier
         * to add data and get times back.
         */


        public string convertStampToDateString(DateTime date)
        {
            string DateString = "";
            DateString = date.ToString("MM/dd/yyyy");
            return DateString;
        }


        public string convertStampToTimeString(DateTime time)
        {
            string TimeString = "";
            TimeString = time.ToString("hh:mm tt");
            return TimeString;
        }


        public DateTime convertStringToDatetime(String date, String time)
        {
            string dateAndTime = date + " " + time;
            DateTime returnDate = Convert.ToDateTime(dateAndTime, CultureInfo.InvariantCulture);
            return returnDate;
        }

        public DateTime convertDateStringToDatetime(String date)
        {     
            DateTime returnDate = Convert.ToDateTime(date, CultureInfo.InvariantCulture);
            return returnDate;
        }


    }


}

