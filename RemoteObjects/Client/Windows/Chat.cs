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

        public Chat() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            chatview.Buffer.Text = global::Mono.Unix.Catalog.GetString("abc");
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            chatService.Exit();
            this.Destroy();
        }

        public User GetDestUser()
        {
            return this.dest;
        }

        protected void OnSendClicked(object sender, EventArgs e)
        {
            //chatService.Send(new Message(src, dest, message.Text));
            AddMessage(new Message(src, dest, message.Text));
            message.Text = "";
        }

        public void AddMessage(Message msg)
        {
            Console.WriteLine(msg.content);
            chatview.Buffer.Text += $"{msg.src.Username}: {msg.content}";
        }

    }
}
