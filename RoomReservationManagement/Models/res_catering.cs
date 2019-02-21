using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
    public class res_catering
    {

        [Key]
        public int cat_id { get; set; }

        public string cat_order { get; set; }

        [ForeignKey("Res_Reservations")]
        public int res_id { get; set; }

        public DateTime res_dt { get; set; }

        public string start_tm { get; set; }

        public string void_ind { get; set; }

        public virtual res_reservations Res_Reservations { get; set; }
    }
}