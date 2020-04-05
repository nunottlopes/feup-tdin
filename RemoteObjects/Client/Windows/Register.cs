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

        protected void OnRegisterClicked(object sender, EventArgs e)
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

        protected void OnExitClicked(object sender, EventArgs e)
        {
            Application.Quit();
        }
    }
}
