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

            message.GrabFocus();

            AddTags();
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            chatService.Exit(this.src);
            WindowManager.getInstance().LeaveChat(this.dest);
            args.RetVal = true;
        }

        protected void OnFocusInEvent(object o, Gtk.FocusInEventArgs args)
        {
            message.GrabFocus();
        }

        public User GetDestUser()
        {
            return this.dest;
        }

        private void AddTags()
        {
            Gtk.TextTag tag = new Gtk.TextTag("default");
            chatview.Buffer.TagTable.Add(tag);

            Gtk.TextTag tag1 = new Gtk.TextTag(src.Username);
            chatview.Buffer.TagTable.Add(tag1);
            tag1.Foreground = "red";
            tag1.Weight = Pango.Weight.Bold;

            Gtk.TextTag tag2 = new Gtk.TextTag(dest.Username);
            chatview.Buffer.TagTable.Add(tag2);
            tag2.Foreground = "blue";
            tag2.Weight = Pango.Weight.Bold;
        }

        protected void OnSendClicked(object sender, EventArgs e)
        {
            SendMessage();
        }

        protected void OnMessageActivated(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            chatService.Send(new Message(src, dest, message.Text));
            AddMessage(new Message(src, dest, message.Text));
            message.Text = "";
        }

        public void AddMessage(Message msg)
        {
            string header = msg.src.Username + ": ";
            string main = msg.content + "\n";

            Gtk.Application.Invoke(delegate
            {
                Gtk.TextIter iter = chatview.Buffer.EndIter;

                chatview.Buffer.InsertWithTagsByName(ref iter, header, msg.src.Username);
                chatview.Buffer.InsertWithTagsByName(ref iter, main, "default");
            });
        }

        protected void OnChatviewSizeAllocated(object o, Gtk.SizeAllocatedArgs args)
        {
            chatview.ScrollToIter(chatview.Buffer.EndIter, 0, false, 0, 0);
        }
    }
}
