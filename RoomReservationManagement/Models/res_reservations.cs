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

        [Required]
        public DateTime res_dt { get; set; }

        [Required]
        public string res_start { get; set; }

        [Required]
        public string res_end { get; set; }

        [ForeignKey("Res_Rooms")]
        public int room_id { get; set; }

        [Required]
        public string res_name { get; set; }

        [Required]
        public string cat_ind { get; set; }


        public string cat_order { get; set; }


        public string void_ind { get; set; }

        public virtual res_rooms Res_Rooms { get; set; }

    }

}