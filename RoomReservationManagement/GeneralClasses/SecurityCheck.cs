using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RoomReservationManagement.GeneralClasses;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.GeneralClasses
{
    public class SecurityCheck 
    {

		/// <summary>
		/// Checks to see if a user has FULL access to the system
		/// </summary>
		/// <returns></returns>
        public Boolean hasFullAccess()
        {

            if((HttpContext.Current.User.IsInRole("RR_Manager") ||
                HttpContext.Current.User.IsInRole("RR_Admin") ||
                HttpContext.Current.User.IsInRole("RR_Secretary")) &&
                HttpContext.Current.Request.IsAuthenticated){
                return true;
            }
            else
            {
                return false;
            }
            
        }

		/// <summary>
		/// Checks to see if a user is a manager
		/// </summary>
		/// <returns></returns>
        public Boolean hasManagerAccess()
        {

            if ((HttpContext.Current.User.IsInRole("RR_Manager") || HttpContext.Current.User.IsInRole("RR_Admin")) && HttpContext.Current.Request.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

		/// <summary>
		/// Checks to see if a user is an admin
		/// </summary>
		/// <returns></returns>
        public Boolean hasAdminAccess()
        {

            if (HttpContext.Current.User.IsInRole("RR_Admin") && HttpContext.Current.Request.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

		/// <summary>
		/// Checks to see if a user is a secretary
		/// </summary>
		/// <returns></returns>
        public Boolean hasSecretaryAccess()
        {

            if ((HttpContext.Current.User.IsInRole("RR_Secretary") || HttpContext.Current.User.IsInRole("RR_Admin")) && HttpContext.Current.Request.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}