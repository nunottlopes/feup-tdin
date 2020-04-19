using System;
using System.Collections.Generic;
using Client.ServerServices;
using Client.Services;
using Common.Authentication;
using Common.Messages;
using Gtk;

namespace Client.Windows
{
    public partial class Users : Gtk.Window
    {
        private readonly User user;
        private readonly AuthServer authServer;
        private readonly ChatManagerServer chatManagerServer;

        public List<User> online { get; }
        private List<(User, IRequestCallback)> requests; //Requests received
        private List<(Guid, User)> groupRequests; //Requests received
        private List<User> requested; //Requests made

        public Users(User user) :
                base(Gtk.WindowType.Toplevel)
        {
            Console.WriteLine("[Username] {0}", user.Username);
            this.Build();
            this.online = new List<User>();
            this.requests = new List<(User, IRequestCallback)>();
            this.groupRequests = new List<(Guid, User)>();
            this.requested = new List<User>();

            this.user = user;

            this.Title = user.Name;

            authServer = new AuthServer();
            authServer.AddOnlineHandler(new OnlineHandler(UpdateOnlineList));

            chatManagerServer = new ChatManagerServer();

            UpdateOnlineList(authServer.GetOnline());
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            WindowManager.getInstance().Logout(user);
            authServer.Logout(user.Username);
            Application.Quit();
            args.RetVal = true;
        }

        protected void OnLogoutClicked(object sender, EventArgs e)
        {
            WindowManager.getInstance().Logout(user);
            authServer.Logout(user.Username);
            Application.Quit();
        }

        private void AddUserOnline(User user)
        {
            online.Add(user);
            Gtk.Application.Invoke(delegate
            {
                userList.Add(GetOnlineGUI(user));
                userList.ShowAll();
            });
        }

        public void AddRequest(User req, IRequestCallback callback)
        {
            requests.Add((req, callback));
            Gtk.Application.Invoke(delegate
            {
                requestList.Add(GetRequestGUI(req, callback));
                requestList.ShowAll();
            });
        }

        private void UpdateOnlineList(List<User> users)
        {
            // Update users online
            List<User> temp = users.FindAll(u => u.Username != user.Username);
            Gtk.Application.Invoke(delegate
            {
                userList.Forall(w => w.Destroy());
            });
            online.Clear();
            foreach (User u in temp)
            {
                this.AddUserOnline(u);
            }

            // Remove offline users from requests
            if (requests.Count != 0 || groupRequests.Count != 0)
            {
                Gtk.Application.Invoke(delegate
                {
                    requestList.Forall(w => w.Destroy());
                });

                groupRequests.RemoveAll(r => !chatManagerServer.ChatExists(r.Item1));
                foreach ((Guid guid, User user) in groupRequests)
                {
                    this.AddGroupChatRequest(guid, user);
                }

                List<(User, IRequestCallback)> temp1 = requests.FindAll(e => online.Contains(e.Item1));
                requests.Clear();
                foreach ((User u, IRequestCallback cb) in temp1)
                {
                    this.AddRequest(u, cb);
                }
            }

            // Remove offline user requested
            requested = requested.FindAll(e => online.Contains(e));

            Gtk.Application.Invoke(delegate
            {
                WindowManager.getInstance().UpdateChats(users);
            });
        }

        private global::Gtk.HBox GetOnlineGUI(User u)
        {
            global::Gtk.HBox user = new global::Gtk.HBox
            {
                Name = u.Username,
                Spacing = 6
            };
            global::Gtk.Label label = new global::Gtk.Label
            {
                LabelProp = u.Name
            };
            user.Add(label);

            global::Gtk.Box.BoxChild bc1 = ((global::Gtk.Box.BoxChild)(user[label]));
            bc1.Position = 0;

            string msg = "Message";
            global::Gtk.Button button = new global::Gtk.Button
            {
                CanFocus = true,
                UseUnderline = true,
            };
            if(this.requested.Contains(u))
            {
                button.Sensitive = false;
                msg = "Requested";
            }
            else if (this.requests.Exists(e => e.Item1.Username == u.Username))
            {
                button.Sensitive = false;
                msg = "Request";
            }

            button.Label = msg;
            button.Clicked += RequestMessageClicked;
            user.Add(button);

            global::Gtk.Box.BoxChild bc2 = ((global::Gtk.Box.BoxChild)(user[button]));
            bc2.Position = 1;
            bc2.Expand = false;
            bc2.Fill = false;

            return user;
        }

        private void RequestMessageClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string username = button.Parent.Name;
            User user = null;
            foreach(User u in online)
            {
                if(u.Username == username)
                {
                    user = u;
                    break;
                }
            }

            if (user == null) return;
            string url = $"tcp://{user.Address}:{user.Port}/Request";

            IRequest request = (IRequest)Activator.GetObject(typeof(IRequest), url);
            request.MakeRequest(this.user, user, new RequestCallback());
            requested.Add(user);

            Console.WriteLine("[Request Sent] {0}", user.Username);

            Gtk.Application.Invoke(delegate
            {
                ((Button)sender).Sensitive = false;
                ((Button)sender).Label = "Requested";
            });
        }

        private global::Gtk.HBox GetRequestGUI(User u, IRequestCallback callback)
        {
            global::Gtk.HBox request = new global::Gtk.HBox
            {
                Spacing = 6
            };
            // Container child request.Gtk.Box+BoxChild
            global::Gtk.Label label = new global::Gtk.Label
            {
                LabelProp = "Chat with " + u.Name
            };
            request.Add(label);

            global::Gtk.Box.BoxChild bc1 = ((global::Gtk.Box.BoxChild)(request[label]));
            bc1.Position = 0;
            // Container child request.Gtk.Box+BoxChild
            global::Gtk.Button button1 = new global::Gtk.Button
            {
                CanFocus = true,
                UseUnderline = true,
                Label = "Accept"
            };
            button1.Clicked += (s, e) => {
                request.Destroy();
                requests.RemoveAll(t => t.Item1.Equals(u));
                Guid guid = System.Guid.NewGuid();
                callback.Accepted(guid, u, this.user);
                WindowManager.getInstance().RequestAccepted(guid, this.user, u);
                Refresh();
            };
            request.Add(button1);

            global::Gtk.Box.BoxChild bc2 = ((global::Gtk.Box.BoxChild)(request[button1]));
            bc2.Position = 1;
            bc2.Expand = false;
            bc2.Fill = false;
            // Container child request.Gtk.Box+BoxChild
            global::Gtk.Button button2 = new global::Gtk.Button
            {
                CanFocus = true,
                Name = "button7",
                UseUnderline = true,
                Label = "Refuse"
            };
            button2.Clicked += (s, e) => {
                request.Destroy();
                requests.RemoveAll(t => t.Item1.Equals(u));
                callback.Refused(u, this.user);
                Refresh();
            };
            request.Add(button2);

            global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(request[button2]));
            w6.Position = 2;
            w6.Expand = false;
            w6.Fill = false;

            return request;
        }

        private global::Gtk.HBox GetGroupChatRequestGUI(Guid guid, User src)
        {
            global::Gtk.HBox request = new global::Gtk.HBox
            {
                Spacing = 6
            };
            // Container child request.Gtk.Box+BoxChild
            global::Gtk.Label label = new global::Gtk.Label
            {
                LabelProp = "Group Chat with " + src.Name
            };
            request.Add(label);

            global::Gtk.Box.BoxChild bc1 = ((global::Gtk.Box.BoxChild)(request[label]));
            bc1.Position = 0;
            // Container child request.Gtk.Box+BoxChild
            global::Gtk.Button button1 = new global::Gtk.Button
            {
                CanFocus = true,
                UseUnderline = true,
                Label = "Accept"
            };
            button1.Clicked += (s, e) => {
                request.Destroy();
                groupRequests.RemoveAll(t => t.Item1.Equals(guid));

                if(chatManagerServer.ChatExists(guid))
                {
                    chatManagerServer.RemoveRequest(guid, this.user);
                    chatManagerServer.AddUserToChat(guid, this.user);
                    WindowManager.getInstance().GroupChatAccepted(guid, this.user);

                    List<User> chatUsers = chatManagerServer.GetUsersInChat(guid);
                    foreach (User u in chatUsers)
                    {
                        if (u.Equals(this.user)) continue;
                        string url = $"tcp://{u.Address}:{u.Port}/GroupRequest";
                        IGroupRequest gr = (IGroupRequest)Activator.GetObject(typeof(IGroupRequest), url);
                        gr.GroupRequestAccepted(guid, this.user);
                    }
                }
                Refresh();
            };
            request.Add(button1);

            global::Gtk.Box.BoxChild bc2 = ((global::Gtk.Box.BoxChild)(request[button1]));
            bc2.Position = 1;
            bc2.Expand = false;
            bc2.Fill = false;
            // Container child request.Gtk.Box+BoxChild
            global::Gtk.Button button2 = new global::Gtk.Button
            {
                CanFocus = true,
                Name = "button7",
                UseUnderline = true,
                Label = "Refuse"
            };
            button2.Clicked += (s, e) => {
                request.Destroy();
                groupRequests.RemoveAll(t => t.Item1.Equals(guid));

                if(chatManagerServer.ChatExists(guid))
                {
                    chatManagerServer.RemoveRequest(guid, this.user);
                    List<User> chatUsers = chatManagerServer.GetUsersInChat(guid);
                    foreach(User u in chatUsers)
                    {
                        string url = $"tcp://{u.Address}:{u.Port}/GroupRequest";
                        IGroupRequest gr = (IGroupRequest)Activator.GetObject(typeof(IGroupRequest), url);
                        gr.GroupRequestRefused(guid, this.user);
                    }
                }
                Refresh();
            };
            request.Add(button2);

            global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(request[button2]));
            w6.Position = 2;
            w6.Expand = false;
            w6.Fill = false;

            return request;
        }

        public void AddGroupChatRequest(Guid guid, User src)
        {
            Gtk.Application.Invoke(delegate
            {
                requestList.Add(GetGroupChatRequestGUI(guid, src));
                requestList.ShowAll();
            });
        }

        public void RemoveRequested(User u)
        {
            requested.RemoveAll(e => e.Equals(u));
            Refresh();
        }

        public void Refresh()
        {
            UpdateOnlineList(online);
        }
    }
}
