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

        public ChatService()
        {
            _connection = new HubConnection("http://tecchathub.azurewebsites.net");
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


            _proxy.On<string, string>("GetMessage", (name, message) => OnMessageReceived(this, new Message
            {
                Name = name,
                Text = message
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
            await RefreshUsersList();
        }

        public async Task Send(Message message, string roomName)
        {
            await _proxy.Invoke("SendMessage", message.Name, message.Text, roomName);
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
