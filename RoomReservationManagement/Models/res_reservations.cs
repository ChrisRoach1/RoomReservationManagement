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

        public DateTime res_dt { get; set; }

        public string res_start { get; set; }

        public string res_end { get; set; }

        [ForeignKey("Res_Rooms")]
        public int room_id { get; set; }

        public string res_name { get; set; }

        public string void_ind { get; set; }

        public virtual res_rooms Res_Rooms { get; set; }

    }

}