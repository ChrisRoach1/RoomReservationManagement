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

		/// <summary>
		/// Function takes in a datetime object and converts it to a string
		/// in the mm/dd/yyyy format
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
        public string convertStampToDateString(DateTime date)
        {
            string DateString = "";
            DateString = date.ToString("MM/dd/yyyy");
            return DateString;
        }

		/// <summary>
		/// Takes in a datetime object and converts it to just a time string
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
        public string convertStampToTimeString(DateTime time)
        {
            string TimeString = "";
            TimeString = time.ToString("hh:mm tt");
            return TimeString;
        }

		/// <summary>
		/// takes in two strings, a date and a time, then
		/// converts them into a datetime object
		/// </summary>
		/// <param name="date"></param>
		/// <param name="time"></param>
		/// <returns></returns>
        public DateTime convertStringToDatetime(String date, String time)
        {
            string dateAndTime = date + " " + time;
            DateTime returnDate = Convert.ToDateTime(dateAndTime, CultureInfo.InvariantCulture);
            return returnDate;
        }

		/// <summary>
		/// Takes in a string thats a date then converts it to a datetime object
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
        public DateTime convertDateStringToDatetime(String date)
        {     
            DateTime returnDate = Convert.ToDateTime(date, CultureInfo.InvariantCulture);
            return returnDate;
        }


    }


}

