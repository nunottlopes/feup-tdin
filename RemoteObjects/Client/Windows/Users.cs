using System;
using System.Collections.Generic;
using Client.ServerServices;
using Common.Authentication;
using Gtk;

namespace Client.Windows
{
    public partial class Users : Gtk.Window
    {
        private readonly User user;
        private readonly AuthServer authServer;
        private List<User> online;

        public Users(User user) :
                base(Gtk.WindowType.Toplevel)
        {
            Console.WriteLine("[Username] {0}", user.Username);
            this.Build();
            this.user = user;

            this.Title = user.Username;

            authServer = new AuthServer();
            authServer.AddOnlineHandler(new OnlineHandler(UpdateOnlineList));

            UpdateOnlineList(authServer.GetOnline());
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
            userList.Add(GetOnlineGUI(user));
            userList.ShowAll();
        }

        private void AddRequest(User req)
        {
            requestList.Add(GetRequestGUI(req));
            requestList.ShowAll();
        }

        private void UpdateOnlineList(List<User> users)
        {
            online = users.FindAll(u => u.Username != user.Username);
            userList.Forall(w => w.Destroy());

            foreach (User u in online)
            {
                this.AddUserOnline(u);
            }
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

            global::Gtk.Button button = new global::Gtk.Button
            {
                CanFocus = true,
                UseUnderline = true,
                Label = global::Mono.Unix.Catalog.GetString("Message")
            };
            user.Add(button);

            global::Gtk.Box.BoxChild bc2 = ((global::Gtk.Box.BoxChild)(user[button]));
            bc2.Position = 1;
            bc2.Expand = false;
            bc2.Fill = false;

            return user;
        }

        private global::Gtk.HBox GetRequestGUI(User u)
        {
            global::Gtk.HBox request = new global::Gtk.HBox
            {
                Name = u.Username + "_request",
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
            request.Add(button2);

            global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(request[button2]));
            w6.Position = 2;
            w6.Expand = false;
            w6.Fill = false;

            return request;
        }
    }
}
