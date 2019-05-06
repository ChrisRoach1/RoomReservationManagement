using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomReservationManagement.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using Moq;

namespace RoomReservationManagement.Test.Controllers
{
	/// <summary>
	/// Summary description for AdminControllerTest
	/// </summary>
	[TestClass]
	public class AdminControllerTest
	{

	

		[TestMethod]
		public void TestManageRoomsView()
		{

			var identity = new GenericIdentity("ChrisRoach");
			var controller = new AdminController();
			var controllerContext = new Mock<ControllerContext>();
			var principal = new Mock<IPrincipal>();
			principal.Setup(p => p.IsInRole("RR_Admin")).Returns(true);
			principal.SetupGet(x => x.Identity.Name).Returns("ChrisRoach");
			controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
			controller.ControllerContext = controllerContext.Object;

			var result = controller.ManageRooms() as ViewResult;
		
			Assert.AreEqual("ManageRooms", result.ViewName);
		}
	}
}
