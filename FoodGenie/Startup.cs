using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodGenie.Startup))]
namespace FoodGenie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
