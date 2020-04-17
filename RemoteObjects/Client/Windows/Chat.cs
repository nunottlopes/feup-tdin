using System;
using Client.Services;
using Common.Authentication;
using Common.Messages;

namespace Client.Windows
{
    public partial class Chat : Gtk.Window
    {
        private User src;

        private User dest;
        private IChat chatService;

        public Chat(User src, User dest) :
                base(Gtk.WindowType.Toplevel)
        {
            this.src = src;
            this.dest = dest;
            this.chatService = new ChatService();
            string url = $"tcp://localhost:{dest.Port}/Chat";
            this.chatService = (IChat)Activator.GetObject(typeof(IChat), url);

            this.Build();
            this.Title = dest.Username;
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
        }

        protected void OnSendClicked(object sender, EventArgs e)
        {
            chatService.Send(new Message(src, dest, message.Text));
        }

    }
}
