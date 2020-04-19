using System;
using Client.ServerServices;
using Gtk;

namespace Client.Windows
{
    public partial class Register : Gtk.Window
    {
        private AuthServer authServer;

        public Register() :
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

        private void RegisterAction()
        {
            bool res = authServer.Register(username.Text, name.Text, password.Text);
            if (!res)
            {
                status.Text = "Failed to Register";
            }
            else
            {
                status.Text = "Register Successful";
                WindowManager.getInstance().Register();
            }
        }

        protected void OnRegisterClicked(object sender, EventArgs e)
        {
            RegisterAction();
        }

        protected void OnExitClicked(object sender, EventArgs e)
        {
            Application.Quit();
        }

        protected void OnFocusInEvent(object o, FocusInEventArgs args)
        {
            username.GrabFocus();
        }

        protected void OnUsernameActivated(object sender, EventArgs e)
        {
            name.GrabFocus();
        }

        protected void OnNameActivated(object sender, EventArgs e)
        {
            password.GrabFocus();
        }

        protected void OnPasswordActivated(object sender, EventArgs e)
        {
            RegisterAction();
        }
    }
}
