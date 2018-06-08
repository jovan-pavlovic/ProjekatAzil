using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjekatAzil.Startup))]
namespace ProjekatAzil
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
