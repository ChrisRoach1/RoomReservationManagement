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

        [Required]
        public string room_name { get; set; }

        [Required]
        public int room_cap { get; set; }

        [Required]
        public int num_comp { get; set; }

        [Required]
        public string proj_ind { get; set; }

        [Required]
        public string void_ind { get; set; }

    }
}