using System;
using Common.Authentication;

namespace Client.Windows
{
    public partial class Chat : Gtk.Window
    {
        private User user;

        public Chat(User u) :
                base(Gtk.WindowType.Toplevel)
        {
            this.user = u;

            this.Build();
            this.Title = u.Username;
        }

        public User GetUser()
        {
            return this.user;
        }

        protected void OnSendClicked(object sender, EventArgs e)
        {

        }
    }
}
