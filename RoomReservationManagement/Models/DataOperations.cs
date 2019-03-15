using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoomReservationManagement.Models;


namespace RoomReservationManagement.Models
{
    public class DataOperations
    {
        /*
         *Class is used to abstract data manipulation.
         * This way controllers do not get cluttered with CRUD logic.
         * Also will easily allow for error catching/handling.
         */


        public ApplicationDbContext databaseConnection = new ApplicationDbContext();

        #region email_ref CRUD

        public int updateEmail(res_email_ref email)
        {
            res_email_ref oldEntry = databaseConnection.Res_Email_refs.Where(e => e.position == email.position).SingleOrDefault();
            oldEntry.email_addr = email.email_addr;
            databaseConnection.SaveChanges();
            return 1;
        }

        #endregion email_ref CRUD


        #region res_reservation_dates CRUD

        public int addReservationDate(res_reservation_dates date)
        {
            databaseConnection.Res_Reservations_Dates.Add(date);
            databaseConnection.SaveChanges();
            return 1;
        }


        #endregion res_reservation_dates CRUD


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
            oldRoom = room;
            databaseConnection.SaveChanges();
            return 1;
        }

        public List<res_rooms> getAllRooms()
        {
            List<res_rooms> roomList = new List<res_rooms>();
            roomList = databaseConnection.Res_Rooms.ToList();
            return roomList;
        }

        #endregion res_rooms CRUD

    }
}