using System;
using Client.ServerServices;
using Common.Authentication;
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
            User res = authServer.Login(username.Text, password.Text);
            if(res == null)
            {
                status.Text = "Failed to Login";
            }
            else
            {
                status.Text = "Login Successful";
                Console.WriteLine("Login as {0}", res);
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
