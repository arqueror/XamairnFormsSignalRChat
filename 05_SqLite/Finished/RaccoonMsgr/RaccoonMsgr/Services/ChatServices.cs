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
        private HubConnection _connection;
        private IHubProxy _proxy;
        private string _username = "";


        public event EventHandler<Models.Message> OnMessageReceived;
        public event EventHandler<ObservableCollection<Contact>> OnConnectedUsersReceived;

        public static Lazy<ChatService> _instanceHolder = new Lazy<ChatService>(() => new ChatService());
        public static ChatService Instance => _instanceHolder.Value;

        public ChatService()
        {
            _connection = new HubConnection("http://tecchathub.azurewebsites.net");
            _proxy = _connection.CreateHubProxy("ChatHub");
            _proxy.On<string, string>("GetMessage", (senderName, message) =>
            {
                if (OnMessageReceived != null)
                    OnMessageReceived(this, new Message()
                    {
                        Name = senderName,
                        Text = message,
                        IsTextIn = true,
                        MessageDateTime = DateTime.Now
                    });
            });

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

                if (OnConnectedUsersReceived != null)
                    OnConnectedUsersReceived(this, connectedContacts);


            });
        }

        #region IChatServices implementation

        public async Task Connect()
        {
            var http = new Microsoft.AspNet.SignalR.Client.Http.DefaultHttpClient();
            if (Application.Current.Properties.ContainsKey("username"))
            {

                _username = Application.Current.Properties["username"] as string;
                _connection.Headers["username"] = _username;
            }
            if (!string.IsNullOrEmpty(_username))
            {
                if (_connection != null && _connection.State != ConnectionState.Connected)
                    await _connection.Start(http);
                if (_connection != null && _connection.State == ConnectionState.Connected)
                    await UpdateUsersOnAllClients();
            }
        }
        public void Disconnect()
        {
            _connection.Stop();


        }
        public async Task SendMessage(string message, string name)
        {
            if (_connection.State == ConnectionState.Disconnected)
            {
                await Connect();
            }
            await _connection.Start();
            await _proxy.Invoke("SendDirectMessage", name, message);
        }
        public async Task UpdateUsersOnAllClients()
        {

            await _proxy.Invoke("UpdateConnectedUsersOnAllClients");
        }
        public async Task SendGroupMessage(string message, string name, string roomName)
        {

            await _proxy.Invoke("SendGroupMessage", name, message, roomName);
        }
        public async Task RefreshUsersList()
        {

            await _proxy.Invoke("GetConnectedUsers");
        }

        public async Task JoinRoom(string roomName)
        {

            await _proxy.Invoke("JoinRoom", roomName);
        }


        #endregion
    }
}
