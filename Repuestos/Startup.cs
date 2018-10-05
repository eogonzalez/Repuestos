using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Repuestos.Startup))]
namespace Repuestos
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
