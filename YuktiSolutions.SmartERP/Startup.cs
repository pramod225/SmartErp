using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YuktiSolutions.SmartERP.Startup))]
namespace YuktiSolutions.SmartERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
