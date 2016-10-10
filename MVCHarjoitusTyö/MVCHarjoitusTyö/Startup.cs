using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCHarjoitusTyö.Startup))]
namespace MVCHarjoitusTyö
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
