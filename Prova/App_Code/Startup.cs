using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prova.Startup))]
namespace Prova
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
