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
        [Display(Name = "Room Name")]
        public string room_name { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string location { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public int room_cap { get; set; }

        [Required]
        [Display(Name = "Computers")]
        public int num_comp { get; set; }

        [Required]
        [Display(Name = "Chairs")]
        public int num_chairs { get; set; }

        [Required]
        [Display(Name = "Tables")]
        public int num_tables { get; set; }

        [Required]
        [Display(Name = "Board Type")]
        public string board_typ { get; set; }

        [Required]
        [Display(Name = "Network")]
        public string network_ind { get; set; }

        [Required]
        [Display(Name = "Teleconnection")]
        public string telecon_sys { get; set; }

        [Required]
        [Display(Name = "Projector")]
        public string proj_ind { get; set; }

        public string void_ind { get; set; }

    }
}