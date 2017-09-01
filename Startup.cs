using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DienMayws.Startup))]
namespace DienMayws
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
