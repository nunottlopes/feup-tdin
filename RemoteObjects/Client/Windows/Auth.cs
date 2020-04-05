using System;
using Client.ServerServices;
using Gtk;

namespace Client.Windows
{
    public partial class Auth : Gtk.Window
    {

        private AuthServer authServer;

        public Auth() :
                base(Gtk.WindowType.Toplevel)
        {
            authServer = new AuthServer();
            this.Build();
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            Application.Quit();
            args.RetVal = true;
        }

        protected void OnExitClicked(object sender, EventArgs e)
        {
            Application.Quit();
        }

        protected void OnLoginClicked(object sender, EventArgs e)
        {
            bool res = authServer.Login(username.Text, password.Text);
            if(!res)
            {
                status.Text = "Failed to Login";
            }
            else
            {
                status.Text = "Login Successful";
                WindowManager.getInstance().Login();
            }
        }

        protected void OnRegisterClicked(object sender, EventArgs e)
        {
            WindowManager.getInstance().Register();
        }

        public void Reset()
        {
            username.Text = "";
            password.Text = "";
            status.Text   = "";
        }
    }
}
