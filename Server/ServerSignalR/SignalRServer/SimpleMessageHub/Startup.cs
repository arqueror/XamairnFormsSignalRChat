using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(SimpleMessageHub.Startup))]

namespace SimpleMessageHub
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
           
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                //without this two way events wont work
                var hubConfiguration = new HubConfiguration() { };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
