using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
    public class res_reservation_dates
    {
        [Key]
        public int resdt_id { get; set; }

        public DateTime day { get; set; }

        public string start_tm { get; set; }

        public string end_tm { get; set; }

        public string void_ind { get; set; }

    }
}