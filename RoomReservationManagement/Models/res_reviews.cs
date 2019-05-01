using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
    public class res_reviews
    {
        [Key]
        public int rev_id { get; set; }

		[Required]
        [ForeignKey("Res_Rooms")]
		[Display(Name = "Room")]
        public int room_id { get; set; }

        [Required]
		[Display(Name = "Review")]
		public string review { get; set; }

        [Required]
		[Display(Name = "Rating")]
		public int rating { get; set; }

        public DateTime audit_create_dt { get; set; }

        public string void_ind { get; set; }


        public virtual res_rooms Res_Rooms { get; set; }





    }
}