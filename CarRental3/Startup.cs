using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRental3.Startup))]
namespace CarRental3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
