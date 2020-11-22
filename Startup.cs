using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Britehouse.Startup))]
namespace Britehouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
