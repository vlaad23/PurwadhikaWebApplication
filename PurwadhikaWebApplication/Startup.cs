using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PurwadhikaWebApplication.Startup))]
namespace PurwadhikaWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
