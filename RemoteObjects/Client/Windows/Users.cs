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
            this.user = user;
            authServer = new AuthServer();
            online = authServer.GetOnline().FindAll(u => u.Username != user.Username);

            this.Build();
            this.Title = user.Username;
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
    }
}
