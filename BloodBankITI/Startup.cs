using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloodBankITI.Startup))]
namespace BloodBankITI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
