using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MakerTableStore.Startup))]
namespace MakerTableStore
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
