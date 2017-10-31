using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRBackEnd.Startup))]

namespace SignalRBackEnd
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.MapSignalR();

        }
    }
}
