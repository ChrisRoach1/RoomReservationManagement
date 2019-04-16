using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using RoomReservationManagement.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using RoomReservationManagement.GeneralClasses;

namespace RoomReservationManagement.Models
{
    public class DataOperations
    {
		/*
         *Class is used to abstract data manipulation.
         * This way controllers do not get cluttered with CRUD logic.
         * Also will easily allow for error catching/handling.
         */

		private ApplicationUserManager _userManager;
		public ApplicationDbContext databaseConnection = new ApplicationDbContext();

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		#region email_ref CRUD

		public int updateEmail(res_email_ref email)
        {
            res_email_ref oldEntry = databaseConnection.Res_Email_refs.Where(e => e.position == email.position).SingleOrDefault();
            oldEntry.email_addr = email.email_addr;
            databaseConnection.SaveChanges();
            return 1;
        }

        public string getSecretaryEmail()
        {
            res_email_ref email = databaseConnection.Res_Email_refs.Where(e => e.position == "Reservation Secretary").SingleOrDefault();
           
            return email.email_addr;
        }

		public string getAdminEmail()
		{
			res_email_ref email = databaseConnection.Res_Email_refs.Where(e => e.position == "IT Admin").SingleOrDefault();

			return email.email_addr;
		}


		#endregion email_ref CRUD

		#region res_reservation CRUD

		public int addReservation(res_reservations reservation)
        {
            databaseConnection.Res_Reservations.Add(reservation);
            databaseConnection.SaveChanges();
            return 1;
        }

        public int deleteReservation(res_reservations reservation)
        {
            res_reservations res = databaseConnection.Res_Reservations.Where(r => r.res_id == reservation.res_id).SingleOrDefault();
            res.void_ind = "y";
            databaseConnection.SaveChanges();
            return 1;
        }

        public List<res_reservations> getAllReservations()
        {
            List<res_reservations> reservationList = new List<res_reservations>();
            reservationList = databaseConnection.Res_Reservations.ToList();
            return reservationList;
        }

        public List<res_reservations> getAllPendingReservations()
        {
            List<res_reservations> reservationList = new List<res_reservations>();
            reservationList = databaseConnection.Res_Reservations.Where(r => r.pending_ind == "y" && r.approved_ind == "n").ToList();
            return reservationList;
        }

        public List<res_reservations> getReservationWithStartTime(DateTime startTime, DateTime endTime, int roomId)
        {
            List<res_reservations> reservationList = new List<res_reservations>();
            reservationList = databaseConnection.Res_Reservations.Where(r => ((r.res_start <= startTime && r.res_end >= startTime) || (r.res_start <= endTime && r.res_end >= endTime)) && r.room_id == roomId && r.reject_ind == "n" && r.void_ind == "n").ToList();
            return reservationList;
        }

        public int rejectReservation(int res_id)
        {
            res_reservations tempRes = databaseConnection.Res_Reservations.SingleOrDefault(r => r.res_id == res_id);
            tempRes.reject_ind = "y";
            tempRes.pending_ind = "n";
            tempRes.approved_ind = "n";
            databaseConnection.SaveChanges();

            return 1;
        }

        public int approveReservation(int res_id)
        {
            res_reservations tempRes = databaseConnection.Res_Reservations.SingleOrDefault(r => r.res_id == res_id);
            tempRes.reject_ind = "n";
            tempRes.pending_ind = "n";
            tempRes.approved_ind = "y";
            databaseConnection.SaveChanges();

            return 1;
        }

        #endregion res_reservation CRUD

        #region res_reviews CRUD

        public int addReview(res_reviews review)
        {
            databaseConnection.Res_Reviews.Add(review);
            databaseConnection.SaveChanges();
            return 1;
        }

        public List<res_reviews> getAllReviews()
        {
            List<res_reviews> reviewsList = new List<res_reviews>();
            reviewsList = databaseConnection.Res_Reviews.ToList();
            return reviewsList;
        }

        #endregion res_reviews CRUD

        #region res_rooms CRUD

        public int addRoom(res_rooms room)
        {
            databaseConnection.Res_Rooms.Add(room);
            databaseConnection.SaveChanges();
            return 1;
        }

        public int deleteRoom(res_rooms room)
        {
            res_rooms oldRoom = databaseConnection.Res_Rooms.Where(r => r.room_id == room.room_id).SingleOrDefault();
            oldRoom.void_ind = "y";
            databaseConnection.SaveChanges();
            return 1;
        }

        public int updateRoom(res_rooms room)
        {
            res_rooms oldRoom = databaseConnection.Res_Rooms.Where(r => r.room_id == room.room_id).SingleOrDefault();
            
            oldRoom.room_name = room.room_name;
            oldRoom.location = room.location;
            oldRoom.room_cap = room.room_cap;
            oldRoom.num_comp = room.num_comp;
            oldRoom.num_chairs = room.num_chairs;
            oldRoom.num_tables = room.num_tables;
            oldRoom.board_typ = room.board_typ;
            oldRoom.network_ind = room.network_ind;
            oldRoom.telecon_sys = room.telecon_sys;
            oldRoom.proj_ind = room.proj_ind;
            databaseConnection.SaveChanges();
            return 1;
        }

        public List<res_rooms> getAllRooms()
        {
            List<res_rooms> roomList = new List<res_rooms>();
            roomList = databaseConnection.Res_Rooms.ToList();
            return roomList;
        }

        public List<res_rooms> getAllAvailableRooms()
        {
            List<res_rooms> roomList = new List<res_rooms>();
            roomList = databaseConnection.Res_Rooms.Where(r => r.void_ind == "n").ToList();
            return roomList;
        }

        public res_rooms getRoom(int id)
        {
            return databaseConnection.Res_Rooms.Where(r => r.room_id == id).SingleOrDefault();
        }

        public int disableRoom(int id)
        {
            res_rooms tempRoom = databaseConnection.Res_Rooms.SingleOrDefault(r => r.room_id == id);
            tempRoom.void_ind = "y";
            databaseConnection.SaveChanges();
            return 1;
        }

        public int enableRoom(int id)
        {
            res_rooms tempRoom = databaseConnection.Res_Rooms.SingleOrDefault(r => r.room_id == id);
            tempRoom.void_ind = "n";
            databaseConnection.SaveChanges();
            return 1;
        }

		#endregion res_rooms CRUD


		#region Users
		public List<ApplicationUser> getAllUsers()
		{
			return databaseConnection.Users.ToList();
		}

		public int deleteUserAccount(string id)
		{

			var user = UserManager.FindById(id);

			var logins = user.Logins;

			var rolesForUser = UserManager.GetRoles(id);

			using (var transaction = databaseConnection.Database.BeginTransaction())
			{
				foreach(var login in logins.ToList())
				{
					UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
				}
				if(rolesForUser.Count() > 0)
				{
					foreach(var role in rolesForUser.ToList())
					{
						var result = UserManager.RemoveFromRole(user.Id, role);
					}
				}
				UserManager.Delete(user);
			}
			return 1;
		}

		public string getUserEmail(string id)
		{
			var user = UserManager.FindById(id);
			return user.Email;
		}

		public int updateUserPassword(PasswordReset reset)
		{
			if (UserManager.HasPassword(reset.userID))
			{
				//UserManager.RemovePassword(reset.userID);
				//UserManager.AddPassword(reset.userID, reset.ConfirmNewPassword);
				var user = UserManager.FindById(reset.userID);
				string oldPass = user.PasswordHash;
				UserManager.ChangePassword(reset.userID, oldPass, reset.ConfirmNewPassword);

			}
			return 1;
		}
		#endregion Users
	}
}