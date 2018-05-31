using System;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System.Linq;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR;
namespace SimpleMessageHub.Hub
{
    public class ChatHub : Microsoft.AspNet.SignalR.Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();

        //Group message
        public void SendGroupMessage(string name, string message, string roomName)
        {
            Clients.Group(roomName).GetGroupMessage(name, message, roomName);
        }

        //Direct message
        public async Task SendDirectMessage(string name, string message)
        {
            //get caller's username
            string callerName = Context.Headers["username"].ToString();
            //Get target user Id
            //var cId = _connections.GetConnections(name).FirstOrDefault();

            //Get all Ids associated to a given username
            var cIds = _connections.GetConnections(name).ToList();


            if (cIds .Count>0)
            {
                //Send to just one device
                //await Clients.Client(cId).GetMessage(callerName, message);
                await Clients.Clients(cIds).GetMessage(callerName, message);
            }
        }
        public async Task IsUsernameAvailable(string name)
        {
            var cId = _connections.GetConnections(name).FirstOrDefault();
            await Clients.Client(Context.ConnectionId).IsUsernameAvailable(cId==null);
        }

        public Task JoinRoom(string roomName)
        {

            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRooom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }
        public async Task UpdateConnectedUsersOnAllClients()
        {
            var serialized= JsonConvert.SerializeObject(_connections);
            await Clients.All.GetConnectedUsers(serialized);
        }
        public async Task GetConnectedUsers()
        {
            string name = Context.Headers["username"].ToString();
            var cId = _connections.GetConnections(name).FirstOrDefault();
            if (cId != null)
            {
                var serialized = JsonConvert.SerializeObject(_connections);
                await Clients.Client(cId).GetConnectedUsers(serialized);
            }
        }

        public override Task OnConnected()
        {
            //string name = Context.User.Identity.Name;
           string name=Context.Headers["username"].ToString();
            _connections.Add(name, Context.ConnectionId);

            //Update connected clients on ALL devices
            GetConnectedUsers();

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // string name = Context.User.Identity.Name;
            string name = Context.Headers["username"].ToString();
            _connections.Remove(name, Context.ConnectionId);
            //Update connected clients on ALL devices
            GetConnectedUsers();
            return base.OnDisconnected(stopCalled);
        }

    }
}