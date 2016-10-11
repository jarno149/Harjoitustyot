using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCHarjoitustyö.Startup))]
namespace MVCHarjoitustyö
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
