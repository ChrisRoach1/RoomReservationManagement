using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
    public class res_email_xref
    {
        [Key]
        public string position { get; set; }
        public string email_addr { get; set; }

    }
}