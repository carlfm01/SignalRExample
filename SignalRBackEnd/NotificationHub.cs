using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace SignalRBackEnd
{
    [HubName("NotificationHub")]
    public class NotificationHub : Hub
    {
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
        public override Task OnConnected()
        {
            return base.OnConnected();  
        }
         

        public void NotifyClients(string message)
        {
            //Envía lo que 1 envió a todo el resto de clientes conectados
             Clients.AllExcept(Context.ConnectionId).NotifyClients(message);
        }


        public void NotifyServer()
        {
            //Sólo el servidor 
        }
    }
}