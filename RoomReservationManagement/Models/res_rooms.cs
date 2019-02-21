using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
    public class res_rooms
    {
        [Key]
        public int room_id { get; set; }

        public string room_name { get; set; }

        public int room_cap { get; set; }

        public int num_comp { get; set; }

        public string proj_ind { get; set; }

        public string void_ind { get; set; }

    }
}