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
        public List<User> online { get; }
        private List<(User, IRequestCallback)> requests; //Requests received
        private List<User> requested; //Requests made

        public Users(User user) :
                base(Gtk.WindowType.Toplevel)
        {
            Console.WriteLine("[Username] {0}", user.Username);
            this.Build();
            this.online = new List<User>();
            this.requests = new List<(User, IRequestCallback)>();
            this.requested = new List<User>();

            this.user = user;

            this.Title = user.Username;

            authServer = new AuthServer();
            authServer.AddOnlineHandler(new OnlineHandler(UpdateOnlineList));

            UpdateOnlineList(authServer.GetOnline());
        }

        public void PrintState()
        {
            //Console.WriteLine("--------Start---------");
            //if (this.requests.Count != 0)
            //{
            //    Console.WriteLine("[Requests List]");
            //    foreach (var item in this.requests)
            //    {
            //        Console.WriteLine("> " + item.Item1.Username);
            //    }
            //}

            //if (this.requested.Count != 0)
            //{
            //    Console.WriteLine("[Resquested List]");
            //    foreach (var user in this.requested)
            //    {
            //        Console.WriteLine("> " + user.Username);
            //    }
            //}

            //Dictionary<Guid, Chat> chatWindows = WindowManager.getInstance().GetChats();
            //if(chatWindows.Count != 0)
            //{
            //    Console.WriteLine("[Chatting List]");
            //    foreach (var chat in chatWindows)
            //    {
            //        //Console.WriteLine("> " + chat.Value.GetDest().Username);
            //    }
            //}

            //Console.WriteLine("---------End----------");
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            authServer.Logout(user.Username);
            Application.Quit(); 
            args.RetVal = true;
        }

        protected void OnLogoutClicked(object sender, EventArgs e)
        {
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

            // Update Chat Windows
            WindowManager.getInstance().UpdateChatWindows(temp);

            // Remove offline users from requests
            if (requests.Count == 0) return;
            List<(User, IRequestCallback)> temp1 = requests.FindAll(e => online.Contains(e.Item1));
            Gtk.Application.Invoke(delegate
            {
                requestList.Forall(w => w.Destroy());
            });
            requests.Clear();
            foreach ((User u, IRequestCallback cb) in temp1)
            {
                this.AddRequest(u, cb);
            }

            this.PrintState();
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
                LabelProp = global::Mono.Unix.Catalog.GetString(u.Username)
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

            button.Label = global::Mono.Unix.Catalog.GetString(msg);
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
            string url = $"tcp://localhost:{user.Port}/Request";

            IRequest request = (IRequest)Activator.GetObject(typeof(IRequest), url);
            request.MakeRequest(this.user, user, new RequestCallback());
            requested.Add(user);

            Console.WriteLine("[Request Sent] {0}", user.Username);

            Gtk.Application.Invoke(delegate
            {
                ((Button)sender).Sensitive = false;
                ((Button)sender).Label = global::Mono.Unix.Catalog.GetString("Requested");
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
                LabelProp = global::Mono.Unix.Catalog.GetString(u.Username)
            };
            request.Add(label);

            global::Gtk.Box.BoxChild bc1 = ((global::Gtk.Box.BoxChild)(request[label]));
            bc1.Position = 0;
            // Container child request.Gtk.Box+BoxChild
            global::Gtk.Button button1 = new global::Gtk.Button
            {
                CanFocus = true,
                UseUnderline = true,
                Label = global::Mono.Unix.Catalog.GetString("Accept")
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
                Label = global::Mono.Unix.Catalog.GetString("Refuse")
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
