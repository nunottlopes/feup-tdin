using System;
using System.Collections.Generic;
using System.Linq;
using Common.Authentication;
using Common.Messages;

namespace Client.Windows
{
    public partial class Chat : Gtk.Window
    {
        private Guid guid;
        private User src;

        private Dictionary<User, IChat> dest;

        public Chat(Guid guid, User src, User dest) :
                base(Gtk.WindowType.Toplevel)
        {
            this.guid = guid;
            this.src = src;
            this.dest = new Dictionary<User, IChat>();

            this.Build();

            message.GrabFocus();

            AddTags();
            AddUser(dest);
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            foreach (IChat chatService in dest.Values)
            {
                chatService.Exit(guid, this.src);
            }

            WindowManager.getInstance().LeaveChat(guid);
        }

        protected void OnFocusInEvent(object o, Gtk.FocusInEventArgs args)
        {
            message.GrabFocus();
        }

        public List<User> GetDestUsers()
        {
            return new List<User>(dest.Keys);
        }

        public void UpdateUsers(List<User> online)
        {
            List<User> temp = new List<User>();
            foreach (User u in dest.Keys)
            {
                if (!online.Contains(u))
                {
                    temp.Add(u);
                }
            }

            foreach (User u in temp)
            {
                dest.Remove(u);
            }

            if (dest.Count == 0)
            {
                WindowManager.getInstance().LeaveChat(guid);
            } else
            {
                UpdateChatUsersGUI();
            }
        }

        public void AddUser(User u)
        {
            string url = $"tcp://localhost:{u.Port}/Chat";
            IChat chatService = (IChat)Activator.GetObject(typeof(IChat), url);
            this.dest.Add(u, chatService);

            CreateTag(u.Username);
            UpdateChatUsersGUI();
        }

        public void RemoveUser(User u)
        {
            dest.Remove(u);
            if (dest.Count == 0)
            {
                WindowManager.getInstance().LeaveChat(guid);
            } else
            {
                UpdateChatUsersGUI();
            }
        }

        private void UpdateChatUsersGUI()
        {
            Gtk.Application.Invoke(delegate
            {
                usersview.Buffer.Text = "";
                Gtk.TextIter iter = usersview.Buffer.EndIter;

                string text = src.Username + " (me)\n";
                usersview.Buffer.InsertWithTagsByName(ref iter, text, src.Username);

                foreach (User u in dest.Keys)
                {
                    text = u.Username + "\n";
                    usersview.Buffer.InsertWithTagsByName(ref iter, text, u.Username);
                }

                if(dest.Count == 1)
                {
                    this.Title = "Chat - " + dest.First().Key.Username;
                } else
                {
                    this.Title = "Group Chat";
                }
            });
        }

        private void AddTags()
        {
            Gtk.TextTag tag = new Gtk.TextTag("default");
            chatview.Buffer.TagTable.Add(tag);

            CreateTag(src.Username);
        }

        private void CreateTag(string name)
        {
            Random r = new Random();

            Gtk.Application.Invoke(delegate
            {
                Gdk.Color c = new Gdk.Color(
                    Convert.ToByte(r.Next(0, 256)),
                    Convert.ToByte(r.Next(0, 256)),
                    Convert.ToByte(r.Next(0, 256)));

                Gtk.TextTag tag = new Gtk.TextTag(name);
                chatview.Buffer.TagTable.Add(tag);
                tag.ForegroundGdk = c;
                tag.Weight = Pango.Weight.Bold;

                Gtk.TextTag tag1 = new Gtk.TextTag(name);
                usersview.Buffer.TagTable.Add(tag1);
                tag1.ForegroundGdk = c;
                tag1.Weight = Pango.Weight.Bold;
            });
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
            if (message.Text.Trim() == "") return;

            foreach(IChat chatService in dest.Values)
            {
                chatService.Send(new Message(guid, src, message.Text));
            }
            AddMessage(new Message(guid, src, message.Text));
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
