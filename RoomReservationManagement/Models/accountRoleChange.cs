using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
	public class accountRoleChange
	{
		[Display(Name = "User Name")]
		public string userName { get; set; }

		public string userID { get; set; }

		[Display(Name = "Old Role")]
		public string oldRole { get; set; }

		[Required]
		[Display(Name = "New Roles")]
		public string newRole { get; set; }

	}
}