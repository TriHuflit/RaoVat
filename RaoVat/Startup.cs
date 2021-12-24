using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RaoVat.StartupOwin))]

namespace RaoVat
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
