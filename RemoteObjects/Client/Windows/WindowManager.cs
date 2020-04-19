using System;
using System.Collections.Generic;
using Client.ServerServices;
using Common.Authentication;
using Common.Messages;

namespace Client.Windows
{
    public class WindowManager
    {
        private static WindowManager instance;
        private enum State { LOGIN, REGISTER, USERS };
        private State state;

        public static WindowManager getInstance()
        {
            if (instance == null)
            {
                instance = new WindowManager();
            }

            return instance;
        }

        private ChatManagerServer chatManagerServer;

        private Auth authWindow;
        private Register registerWindow;
        public Users usersWindow { get; set; }

        private Dictionary<Guid, Chat> chatWindows;

        public WindowManager()
        {
            state = State.LOGIN;
            authWindow = new Auth();

            authWindow.Show();
        }

        public void Register()
        {
            if(state == State.LOGIN)
            {
                authWindow.Hide();
                registerWindow = new Register();
                registerWindow.Show();
                state = State.REGISTER;
            } else if(state == State.REGISTER)
            {
                registerWindow.Destroy();
                authWindow.Reset();
                authWindow.Show();
                state = State.LOGIN;
            }
        }

        public void Login(User user)
        {
            if(state == State.LOGIN)
            {
                authWindow.Destroy();
                chatManagerServer = new ChatManagerServer();
                chatWindows = new Dictionary<Guid, Chat>();
                usersWindow = new Users(user);
                usersWindow.Show();
                state = State.USERS;
            }
        }

        public void Logout(User user)
        {
            chatManagerServer.RemoveUser(user);
        }

        public void RequestReceived(User src, IRequestCallback callback)
        {
            if(state == State.USERS)
            {
                usersWindow.AddRequest(src, callback);
                usersWindow.Refresh();
            }
        }

        public void RequestAccepted(Guid guid, User src, User dest)
        {
            Console.WriteLine("[Chatting] {0}", dest.Username);
            Gtk.Application.Invoke(delegate
            {
                chatManagerServer.AddChat(guid, new List<User> { src, dest });
                Chat chat = new Chat(guid, src);
                chatWindows.Add(guid, chat);
                chat.Show();
                usersWindow.RemoveRequested(dest);
            }); 
        }

        public void GroupChatAccepted(Guid guid, User src)
        {
            Gtk.Application.Invoke(delegate
            {
                Chat chat = new Chat(guid, src);
                chatWindows.Add(guid, chat);
                chat.Show();
            });
        }

        public void RequestRefused(User src, User dest)
        {
            usersWindow.RemoveRequested(dest);
        }

        public void MessageReceived(Message msg)
        {
            chatWindows[msg.guid].AddMessage(msg);
        }

        public void LeaveChat(Guid guid, User u)
        {
            Console.WriteLine("[Leave Chat] {0}", guid);
            Gtk.Application.Invoke(delegate
            {
                chatWindows[guid].Destroy();
                chatWindows.Remove(guid);
            });
        }

        public void UpdateChats(List<User> online)
        {
            foreach(Chat c in chatWindows.Values)
            {
                c.Refresh(online);
            }
        }

        public void UpdateChat(Guid guid)
        {
            chatWindows[guid].Refresh(usersWindow.online);
        }

        public Dictionary<Guid, Chat> GetChats()
        {
            return chatWindows;
        }

        internal void AddGroupChatRequest(Guid guid, User src, User dest)
        {
            usersWindow.AddGroupChatRequest(guid, src);
        }

        internal void GroupRequestMade(Guid guid, User u)
        {
            chatWindows[guid].AddRequest(u);
        }
    }
}
