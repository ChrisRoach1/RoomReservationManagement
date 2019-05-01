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
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;


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

		public class userRoles
		{
			public string roleName { get; set; }
			public string roleID { get; set; }
		}

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

		/// <summary>
		/// Update an email address in the email_ref table
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public int updateEmail(res_email_ref email)
        {
            res_email_ref oldEntry = databaseConnection.Res_Email_refs.Where(e => e.position == email.position).SingleOrDefault();
            oldEntry.email_addr = email.email_addr;
            databaseConnection.SaveChanges();
            return 1;
        }

		/// <summary>
		/// get the email of the CEO secretary
		/// </summary>
		/// <returns></returns>
        public string getCEOSecretaryEmail()
        {
            res_email_ref email = databaseConnection.Res_Email_refs.Where(e => e.position == "CEO-Secretary").SingleOrDefault();
           
            return email.email_addr;
        }

		/// <summary>
		/// get the email of the default secretary
		/// </summary>
		/// <returns></returns>
		public string getDefaultSecretaryEmail()
		{
			res_email_ref email = databaseConnection.Res_Email_refs.Where(e => e.position == "Reservation Secretary").SingleOrDefault();

			return email.email_addr;
		}

		/// <summary>
		/// get the email of the nurse secretary
		/// </summary>
		/// <returns></returns>
		public string getNurseSecretaryEmail()
		{
			res_email_ref email = databaseConnection.Res_Email_refs.Where(e => e.position == "Nurse-Secretary").SingleOrDefault();

			return email.email_addr;
		}

		/// <summary>
		/// get the email of the staffdev secretary
		/// </summary>
		/// <returns></returns>
		public string getStaffDevSecretaryEmail()
		{
			res_email_ref email = databaseConnection.Res_Email_refs.Where(e => e.position == "StaffDev-Secretary").SingleOrDefault();

			return email.email_addr;
		}


		/// <summary>
		/// get the email address of an admin
		/// </summary>
		/// <returns></returns>
		public string getAdminEmail()
		{
			res_email_ref email = databaseConnection.Res_Email_refs.Where(e => e.position == "IT Admin").SingleOrDefault();

			return email.email_addr;
		}


		#endregion email_ref CRUD

		#region res_reservation CRUD

		/// <summary>
		/// Function takes in a reservation model and adds it to the database
		/// </summary>
		/// <param name="reservation"></param>
		/// <returns></returns>
		public int addReservation(res_reservations reservation)
        {
            databaseConnection.Res_Reservations.Add(reservation);
            databaseConnection.SaveChanges();
            return 1;
        }

		/// <summary>
		/// will "delete" a reservation, however deleting it
		/// just means setting the void_ind to 'y' so the record 
		/// still exists
		/// </summary>
		/// <param name="reservation"></param>
		/// <returns></returns>
        public int deleteReservation(res_reservations reservation)
        {
            res_reservations res = databaseConnection.Res_Reservations.Where(r => r.res_id == reservation.res_id).SingleOrDefault();
            res.void_ind = "y";
            databaseConnection.SaveChanges();
            return 1;
        }

		/// <summary>
		/// gets all reservations and returns it as a list
		/// </summary>
		/// <returns></returns>
        public List<res_reservations> getAllReservations()
        {
            List<res_reservations> reservationList = new List<res_reservations>();
            reservationList = databaseConnection.Res_Reservations.ToList();
            return reservationList;
        }

		/// <summary>
		/// get all the pending reservations 
		/// </summary>
		/// <returns></returns>
        public List<res_reservations> getAllPendingReservations()
        {
            List<res_reservations> reservationList = new List<res_reservations>();
            reservationList = databaseConnection.Res_Reservations.Where(r => r.pending_ind == "y" && r.approved_ind == "n" && r.void_ind == "n").ToList();
            return reservationList;
        }

		/// <summary>
		/// get all the accepted reservations
		/// </summary>
		/// <returns></returns>
		public List<res_reservations> getAllAcceptedReservations()
		{
			List<res_reservations> reservationList = new List<res_reservations>();
			reservationList = databaseConnection.Res_Reservations.Where(r => r.approved_ind == "y" && r.void_ind == "n").ToList();
			return reservationList;
		}

		/// <summary>
		/// This function is used to verify there are no reservations
		/// at the same time as a reservation being made.
		/// It returns a list of all reservations for a specific start and end time for 
		/// a specific room
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="roomId"></param>
		/// <returns></returns>
		public List<res_reservations> getReservationWithStartTime(DateTime startTime, DateTime endTime, int roomId)
        {
            List<res_reservations> reservationList = new List<res_reservations>();
            reservationList = databaseConnection.Res_Reservations.Where(r => ((r.res_start <= startTime && r.res_end >= startTime) || (r.res_start <= endTime && r.res_end >= endTime)) && r.room_id == roomId && r.reject_ind == "n" && r.void_ind == "n").ToList();
            return reservationList;
        }

		/// <summary>
		/// Function rejects a pending reservation, setting the rejected_ind to 'y'
		/// </summary>
		/// <param name="res_id"></param>
		/// <returns></returns>
        public int rejectReservation(int res_id)
        {
            res_reservations tempRes = databaseConnection.Res_Reservations.SingleOrDefault(r => r.res_id == res_id);
            tempRes.reject_ind = "y";
            tempRes.pending_ind = "n";
            tempRes.approved_ind = "n";
            databaseConnection.SaveChanges();

            return 1;
        }

		/// <summary>
		/// approves a pending reservation, setting the approved_ind to 'y'
		/// </summary>
		/// <param name="res_id"></param>
		/// <returns></returns>
        public int approveReservation(int res_id)
        {
            res_reservations tempRes = databaseConnection.Res_Reservations.SingleOrDefault(r => r.res_id == res_id);
            tempRes.reject_ind = "n";
            tempRes.pending_ind = "n";
            tempRes.approved_ind = "y";
            databaseConnection.SaveChanges();

            return 1;
        }

		/// <summary>
		/// Function will get the email_addr on a specific reservation
		/// </summary>
		/// <param name="res_id"></param>
		/// <returns></returns>
		public string getEmailOnReservation(int res_id)
		{
			res_reservations tempData = databaseConnection.Res_Reservations.Where(r => r.res_id == res_id).SingleOrDefault();
			return tempData.email_addr;
		}

		public res_reservations getReservation(int res_id)
		{
			return databaseConnection.Res_Reservations.Where(r => r.res_id == res_id).SingleOrDefault();
		}

        #endregion res_reservation CRUD

        #region res_reviews CRUD

		/// <summary>
		/// takes in a review model object and adds it to the database
		/// </summary>
		/// <param name="review"></param>
		/// <returns></returns>
        public int addReview(res_reviews review)
        {
            databaseConnection.Res_Reviews.Add(review);
            databaseConnection.SaveChanges();
            return 1;
        }

		/// <summary>
		/// gets all the reviews and returns it as a list
		/// </summary>
		/// <returns></returns>
        public List<res_reviews> getAllReviews()
        {
            List<res_reviews> reviewsList = new List<res_reviews>();
            reviewsList = databaseConnection.Res_Reviews.ToList();
            return reviewsList;
        }

		/// <summary>
		/// get specific 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public res_reviews getReview(int id)
		{
			return databaseConnection.Res_Reviews.Where(r => r.rev_id == id).SingleOrDefault();
		}

        #endregion res_reviews CRUD

        #region res_rooms CRUD

		/// <summary>
		/// takes in a room model object and adds it to the database
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
        public int addRoom(res_rooms room)
        {
            databaseConnection.Res_Rooms.Add(room);
            databaseConnection.SaveChanges();
            return 1;
        }

		/// <summary>
		/// Will void out a designated room
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
        public int deleteRoom(res_rooms room)
        {
            res_rooms oldRoom = databaseConnection.Res_Rooms.Where(r => r.room_id == room.room_id).SingleOrDefault();
            oldRoom.void_ind = "y";
            databaseConnection.SaveChanges();
            return 1;
        }

		/// <summary>
		/// Will update a designated room to the new given values
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Gets all the rooms and returns it as a list
		/// </summary>
		/// <returns></returns>
        public List<res_rooms> getAllRooms()
        {
            List<res_rooms> roomList = new List<res_rooms>();
            roomList = databaseConnection.Res_Rooms.Where(r => r.void_ind == "n").ToList();
            return roomList;
        }

		/// <summary>
		/// Gets all AVAILABLE rooms (i.e. void_ind = 'n')
		/// </summary>
		/// <returns></returns>
        public List<res_rooms> getAllAvailableRooms()
        {
            List<res_rooms> roomList = new List<res_rooms>();
            roomList = databaseConnection.Res_Rooms.Where(r => r.void_ind == "n").ToList();
            return roomList;
        }

		/// <summary>
		/// Get a specific room back
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public res_rooms getRoom(int id)
        {
            return databaseConnection.Res_Rooms.Where(r => r.room_id == id).SingleOrDefault();
        }

		/// <summary>
		/// Function is used to disable a room
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public int disableRoom(int id)
        {
            res_rooms tempRoom = databaseConnection.Res_Rooms.SingleOrDefault(r => r.room_id == id);
            tempRoom.void_ind = "y";
            databaseConnection.SaveChanges();
            return 1;
        }

		/// <summary>
		/// Function enables a room
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public int enableRoom(int id)
        {
            res_rooms tempRoom = databaseConnection.Res_Rooms.SingleOrDefault(r => r.room_id == id);
            tempRoom.void_ind = "n";
            databaseConnection.SaveChanges();
            return 1;
        }

		#endregion res_rooms CRUD

		#region Users
		/// <summary>
		/// Gets all the current users in the users table
		/// This will also return the accounts that are used on the 
		/// other two applications, so be careful.
		/// </summary>
		/// <returns></returns>
		public List<ApplicationUser> getAllUsers()
		{
			return databaseConnection.Users.ToList();
		}

		/// <summary>
		/// Takes in a user id and deletes the user from the system completely
		/// there is no undoing this so please be careful
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Gets all the roles in the system, will also get the roles from
		/// the other two systems
		/// </summary>
		/// <returns></returns>
		public List<userRoles> getAllRoles()
		{
			var allRoles = databaseConnection.Roles.ToList();
			List<userRoles> roles = new List<userRoles>();
			
			foreach(var role in allRoles)
			{
				roles.Add(new userRoles
				{
					roleID = role.Id,
					roleName = role.Name
				});
			}
			return roles;
		}

		/// <summary>
		/// Get the username/email for a specific user id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public string getUserEmail(string id)
		{
			var user = UserManager.FindById(id);
			return user.Email;
		}

		/// <summary>
		/// takes in a passwordReset model object and changes 
		/// the password for the designated user account
		/// </summary>
		/// <param name="reset"></param>
		/// <returns></returns>
		public async Task<int> updateUserPassword(PasswordReset reset)
		{
			if (UserManager.HasPassword(reset.userID))
			{
				
			
				//UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
				
				//string newPassword = reset.ConfirmNewPassword;
				//String hashedNewPassword = UserManager.PasswordHasher.HashPassword(newPassword);
				//await store.SetPasswordHashAsync(user, hashedNewPassword);
				//await store.UpdateAsync(user);

				return 1;
			}
			return 1;
		}

		/// <summary>
		/// Takes in an accountRoleChange model object and then updates the 
		/// designated user with the new chosen role
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		public int updateUserRole(accountRoleChange account)
		{
			UserManager.RemoveFromRole(account.userID, account.oldRole);

			UserManager.AddToRole(account.userID, account.newRole);

			return 1;
		}
		#endregion Users

		#region data_analytics

		/// <summary>
		/// used in data analytics page, pulls in data to show room frequency
		/// </summary>
		/// <returns></returns>
		public List<roomFrequency> getRoomFrequency()
		{
			List<roomFrequency> temp = new List<roomFrequency>();



			var tempQuery = (from room in databaseConnection.Res_Rooms
						join res in databaseConnection.Res_Reservations on room.room_id equals res.room_id
						group room by room.room_name into g
						select new roomFrequency
						{
							roomName = g.Key,
							timesUsed = g.Count()
						}).ToList();

			temp = tempQuery;
			return temp;
		}


		/// <summary>
		/// used in the data analytics page, shows the average review rating for each room
		/// </summary>
		/// <returns></returns>
		public List<roomReviewData> getRoomReviews()
		{
			List<roomReviewData> temp = new List<roomReviewData>();



			var tempQuery = (from room in databaseConnection.Res_Rooms
							 join rev in databaseConnection.Res_Reviews on room.room_id equals rev.room_id
							 group rev by room.room_name into g
							 select new roomReviewData
							 {
								 roomName = g.Key,
								 averageRating = g.Sum(i => i.rating) / g.Count()
							 }).ToList();

			temp = tempQuery;
			return temp;
		}


		#endregion data_analytics

	}
}