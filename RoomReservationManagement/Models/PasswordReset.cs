using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservationManagement.Models
{
	public class PasswordReset
	{

		[Display(Name = "Email")]
		public string Email { get; set; }

		public string userID { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string newPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("newPassword", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmNewPassword { get; set; }
	}
}