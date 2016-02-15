using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Karin.Web.Startup))]
namespace Karin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
