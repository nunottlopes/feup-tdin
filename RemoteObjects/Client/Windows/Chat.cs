using System;
using System.Collections.Generic;
using System.Linq;
using Client.ServerServices;
using Common.Authentication;
using Common.Messages;

namespace Client.Windows
{
    public partial class Chat : Gtk.Window
    {
        private Guid guid;
        private User src;

        private ChatManagerServer chatManagerServer;

        private Dictionary<User, IChat> dest;

        private List<string> tags;

        public Chat(Guid guid, User src) :
                base(Gtk.WindowType.Toplevel)
        {
            this.guid = guid;
            this.src = src;
            this.dest = new Dictionary<User, IChat>();
            this.tags = new List<string>();

            this.chatManagerServer = new ChatManagerServer();

            this.Build();

            message.GrabFocus();

            AddTags();
            Refresh(WindowManager.getInstance().usersWindow.online);
        }

        protected void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            chatManagerServer.RemoveUserFromChat(guid, src);
            foreach (IChat chatService in dest.Values)
            {
                chatService.Exit(guid, this.src);
            }
            WindowManager.getInstance().LeaveChat(guid, src);
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
            online = online.FindAll(u => !u.Equals(src));

            dest.Clear();
            List<User> temp = chatManagerServer.GetUsersInChat(guid);
            temp.RemoveAll(u => u.Equals(src));

            if (temp.Count == 0)
            {
                chatManagerServer.RemoveUserFromChat(guid, src);
                WindowManager.getInstance().LeaveChat(guid, src);
                return;
            }

            foreach (User u in temp)
            {
                AddUser(u);
            }
            UpdateChatUsersGUI();

            // Update online users list
            Gtk.Application.Invoke(delegate
            {
                onlinelist.Forall(w => w.Destroy());
            });

            foreach (User u in online)
            {
                if(!(u.Equals(src) || dest.ContainsKey(u)))
                {
                    this.AddUserOnline(u);
                } 
            }
        }

        public void AddUser(User u)
        {
            string url = $"tcp://{u.Address}:{u.Port}/Chat";
            IChat chatService = (IChat)Activator.GetObject(typeof(IChat), url);
            this.dest.Add(u, chatService);

            CreateTag(u.Username);
        }

        public void AddUserOnline(User u)
        {
            Gtk.Application.Invoke(delegate
            {
                onlinelist.Add(GetOnlineGUI(u));
                onlinelist.ShowAll();
            });
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
                if (tags.Contains(name)) return;

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

                tags.Add(name);
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

        private global::Gtk.HBox GetOnlineGUI(User u)
        {
            global::Gtk.HBox user = new global::Gtk.HBox
            {
                Name = u.Username,
                Spacing = 6
            };
            global::Gtk.Label label = new global::Gtk.Label
            {
                LabelProp = global::Mono.Unix.Catalog.GetString(u.Username)
            };
            user.Add(label);

            global::Gtk.Box.BoxChild bc1 = ((global::Gtk.Box.BoxChild)(user[label]));
            bc1.Position = 0;

            global::Gtk.Button button = new global::Gtk.Button
            {
                CanFocus = true,
                UseUnderline = true,
                Label = global::Mono.Unix.Catalog.GetString("Add")
            };
            if (chatManagerServer.GetRequests(this.guid).Contains(u))
            {
                button.Sensitive = false;
            }

            button.Clicked += AddClicked;
            user.Add(button);

            global::Gtk.Box.BoxChild bc2 = ((global::Gtk.Box.BoxChild)(user[button]));
            bc2.Position = 1;
            bc2.Expand = false;
            bc2.Fill = false;

            return user;
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Gtk.Button button = (Gtk.Button)sender;
            string username = button.Parent.Name;
            User user = WindowManager.getInstance().usersWindow.online.Find(x => x.Username == username);

            if (user == null) return;
            string url = $"tcp://{user.Address}:{user.Port}/GroupRequest";

            IGroupRequest request = (IGroupRequest)Activator.GetObject(typeof(IGroupRequest), url);
            request.MakeRequest(this.guid, src, user);
            chatManagerServer.AddRequest(guid, user);

            foreach (IChat ichat in dest.Values)
            {
                ichat.RequestMade(this.guid, user);
            }

            Console.WriteLine("[Request Add] {0}", user.Username);

            Gtk.Application.Invoke(delegate
            {
                button.Sensitive = false;
            });
        }

        public void AddRequest(User u)
        {
            Refresh(WindowManager.getInstance().usersWindow.online);
        }

        public void Refresh(List<User> online)
        {
            UpdateUsers(online);
        }
    }
}
