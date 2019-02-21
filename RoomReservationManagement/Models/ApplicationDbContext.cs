﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;

namespace RoomReservationManagement.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
    : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //all database model references will be declared here
        #region tables
        public virtual DbSet<error_log> Error_Logs { get; set; }
        public virtual DbSet<res_catering> Res_Caterings { get; set; }
        public virtual DbSet<res_email_xref> Res_Email_Xrefs { get; set; }
        public virtual DbSet<res_reservation_dates> Res_Reservations_Dates { get; set; }
        public virtual DbSet<res_reservations> Res_Reservations { get; set; }
        public virtual DbSet<res_reviews> Res_Reviews { get; set; }
        public virtual DbSet<res_rooms> Res_Rooms { get; set; }

        #endregion tables

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}
