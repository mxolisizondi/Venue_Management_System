using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Venue_Management_System.Startup))]
namespace Venue_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
