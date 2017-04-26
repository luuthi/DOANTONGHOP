using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nongsanxanh.Startup))]
namespace nongsanxanh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
