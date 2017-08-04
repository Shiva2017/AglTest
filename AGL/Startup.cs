using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AGL.Startup))]
namespace AGL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
