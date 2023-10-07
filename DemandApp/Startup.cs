using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemandApp.Startup))]
namespace DemandApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
