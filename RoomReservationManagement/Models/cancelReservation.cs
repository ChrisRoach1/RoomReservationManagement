using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
	public class cancelReservation
	{

		public int res_id { get; set; }

		[Display(Name = "Reasoning")]
		public string cancelReason { get; set; }

	}
}