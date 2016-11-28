using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pip_air.Startup))]
namespace pip_air
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
