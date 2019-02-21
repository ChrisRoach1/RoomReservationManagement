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

        [ForeignKey("Res_Rooms")]
        public int room_id { get; set; }

        public string review { get; set; }

        public string void_ind { get; set; }

        public virtual res_rooms Res_Rooms { get; set; }





    }
}