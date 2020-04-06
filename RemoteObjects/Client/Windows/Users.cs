using System;
using Client.ServerServices;
using Common.Authentication;
using Gtk;

namespace Client.Windows
{
    public partial class Users : Gtk.Window
    {
        private User user;
        private AuthServer authServer;

        public Users(User user) :
                base(Gtk.WindowType.Toplevel)
        {
            this.user = user;
            authServer = new AuthServer();

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
