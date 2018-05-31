using System;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;
using System.Threading.Tasks;
using RaccoonMsgr.ChatServices;
using RaccoonMsgr.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.DeviceInfo;


namespace RaccoonMsgr.ChatServices
{
    public class ChatService
    {
        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;
        private string _username = "";

        public event EventHandler<Models.Message> OnMessageReceived;
        public event EventHandler<ObservableCollection<Contact>> OnConnectedUsersReceived;
        static readonly Lazy<ChatService> _instanceHolder =
            new Lazy<ChatService>(() => new ChatService());
        public static ChatService Instance => _instanceHolder.Value;

        public ChatService()
        {
#if __ANDROID__
            _connection = new HubConnection("ws://10.0.2.2:5000);
#else
            _connection = new HubConnection("http://tecchathub.azurewebsites.net");
#endif
            _proxy = _connection.CreateHubProxy("ChatHub");
        }

        #region IChatServices implementation

        public async Task Connect()
        {
            if (Application.Current.Properties.ContainsKey("username"))
            {

                _username = Application.Current.Properties["username"] as string;
                _connection.Headers["username"] = _username;
                // do something with id
            }


            _proxy.On<string, string>("GetMessage", (senderName, message) => OnMessageReceived(this, new Message
            {
                Name = senderName,
                Text = message,
                IsTextIn = true,
                MessageDateTime = DateTime.Now
            }));

            _proxy.On<string>("GetConnectedUsers", (usersList) =>
            {
                var users = JsonConvert.DeserializeObject<ConnectionMapping<string>>(usersList);
                var connectedContacts = new ObservableCollection<Contact>();
                users.Connections.ForEach(c =>
                {
                    if (c.Key != _username)
                    {
                        connectedContacts.Add(new Contact()
                        {
                            Name = c.Key,
                            ConnectionId = c.Value.First()  //Only taking first element since we dont support multiple devices
                        });
                    }
                });


                OnConnectedUsersReceived(this, connectedContacts);


            });
            await _connection.Start();
            await UpdateUsersOnAllClients();
        }

        public async Task SendMessage(string message, string name)
        {
            if (_connection.State == ConnectionState.Disconnected)
                await _connection.Start();
            await _proxy.Invoke("SendDirectMessage", name, message);
        }
        public async Task UpdateUsersOnAllClients()
        {
            if (_connection.State == ConnectionState.Disconnected)
                await _connection.Start();
            await _proxy.Invoke("UpdateConnectedUsersOnAllClients");
        }
        public async Task SendGroupMessage(string message, string name, string roomName)
        {
            if (_connection.State == ConnectionState.Disconnected)
                await _connection.Start();
            await _proxy.Invoke("SendGroupMessage", name, message, roomName);
        }
        public async Task RefreshUsersList()
        {
            if (_connection.State == ConnectionState.Disconnected)
                await _connection.Start();
            await _proxy.Invoke("GetConnectedUsers");
        }

        public async Task JoinRoom(string roomName)
        {
            if (_connection.State == ConnectionState.Disconnected)
                await _connection.Start();
            await _proxy.Invoke("JoinRoom", roomName);
        }


        #endregion
    }
}
