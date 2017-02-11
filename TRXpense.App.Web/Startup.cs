using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TRXpense.App.Web.Startup))]
namespace TRXpense.App.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
