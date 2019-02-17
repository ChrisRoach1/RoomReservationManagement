using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoomReservationManagement.Startup))]
namespace RoomReservationManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
