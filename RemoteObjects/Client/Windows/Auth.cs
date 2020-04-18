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

        private void Login()
        {
            User user = authServer.Login(username.Text, password.Text);
            if (user == null)
            {
                status.Text = "Failed to Login";
                ResetPassword();
            }
            else
            {
                status.Text = "Login Successful";
                WindowManager.getInstance().Login(user);
            }
        }

        protected void OnLoginClicked(object sender, EventArgs e)
        {
            Login();
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

        public void ResetPassword()
        {
            password.Text = "";
        }

        protected void OnPasswordActivated(object sender, EventArgs e)
        {
            Login();
        }

        protected void OnUsernameActivated(object sender, EventArgs e)
        {
            password.GrabFocus();
        }
    }
}
