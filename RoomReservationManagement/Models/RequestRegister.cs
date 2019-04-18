using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
	public class RequestRegister
	{

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Email")]
		public string email { get; set; }

		[Display(Name = "Reason")]
		public string reqReason { get; set; }
	}
}