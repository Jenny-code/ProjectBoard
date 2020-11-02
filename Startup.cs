using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectBoard.Startup))]
namespace ProjectBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
