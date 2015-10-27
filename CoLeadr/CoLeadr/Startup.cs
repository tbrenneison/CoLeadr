using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoLeadr.Startup))]
namespace CoLeadr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
