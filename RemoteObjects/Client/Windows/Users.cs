﻿using System;
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
        private List<User> online;
        private List<(User, IRequestCallback)> requests;

        public Users(User user) :
                base(Gtk.WindowType.Toplevel)
        {
            Console.WriteLine("[Username] {0}", user.Username);
            this.Build();
            this.online = new List<User>();
            this.requests = new List<(User, IRequestCallback)>();

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
            online.Add(user);
            userList.Add(GetOnlineGUI(user));
            userList.ShowAll();
        }

        public void AddRequest(User req, IRequestCallback callback)
        {
            requests.Add((req, callback));
            Console.WriteLine(req);
            requestList.Add(GetRequestGUI(req, callback));
            requestList.ShowAll();
        }

        private void UpdateOnlineList(List<User> users)
        {
            // Update users online
            List<User> temp = users.FindAll(u => u.Username != user.Username);
            userList.Forall(w => w.Destroy());
            online.Clear();
            foreach (User u in temp)
            {
                this.AddUserOnline(u);
            }

            // Remove offline users from requests
            List<(User, IRequestCallback)> temp1 = requests.FindAll(e => temp.Contains(e.Item1));
            requestList.Forall(w => w.Destroy());
            requests.Clear();
            foreach ((User u, IRequestCallback cb) in temp1)
            {
                this.AddRequest(u, cb);
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

            Console.WriteLine("[Request Sent] {1}", this.user, user);

            ((Button)sender).Sensitive = false;
            ((Button)sender).Label = global::Mono.Unix.Catalog.GetString("Requested");
        }

        private global::Gtk.HBox GetRequestGUI(User u, IRequestCallback callback)
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
            button1.Clicked += (s, e) => callback.Accepted();
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
            button2.Clicked += (s, e) => callback.Refused();
            request.Add(button2);

            global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(request[button2]));
            w6.Position = 2;
            w6.Expand = false;
            w6.Fill = false;

            return request;
        }
    }
}
