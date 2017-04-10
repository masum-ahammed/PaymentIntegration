using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(User.Web.Startup))]
namespace User.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
