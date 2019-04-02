using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
    public class res_reservations
    {
        [Key]
        public int res_id { get; set; }

        
        public string user_id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime res_dt { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime res_start { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime res_end { get; set; }

        [ForeignKey("Res_Rooms")]
        [Display(Name = "Room")]
        public int room_id { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string res_name { get; set; }

        public string cat_ind { get; set; }

        [Display(Name = "Catering Order")]
        public string cat_order { get; set; }

        public int ver_code { get; set; }

        [Required]
        public string pending_ind { get; set; }

        [Required]
        public string approved_ind { get; set; }

        [Required]
        public string reject_ind { get; set; }

        public DateTime audit_create_dt { get; set; }

        [Required]
        public string void_ind { get; set; }

        public virtual res_rooms Res_Rooms { get; set; }

        [ForeignKey("user_id")]
        public virtual ApplicationUser User { get; set; }

    }

}