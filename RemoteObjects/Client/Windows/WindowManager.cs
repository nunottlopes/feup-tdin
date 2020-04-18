using System;
using System.Collections.Generic;
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

        private Auth authWindow;
        private Register registerWindow;
        private Users usersWindow;
        private Dictionary<string, Chat> chatWindows;

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
                chatWindows = new Dictionary<string, Chat>();
                usersWindow = new Users(user);
                usersWindow.Show();
                state = State.USERS;
            }
        }

        public void RequestReceived(User src, IRequestCallback callback)
        {
            if(state == State.USERS)
            {
                usersWindow.AddRequest(src, callback);
                usersWindow.Refresh();
            }
        }

        public void RequestAccepted(User src, User dest)
        {
            Console.WriteLine("[Chatting] {0}", dest.Username);
            usersWindow.RemoveRequested(dest);
            Gtk.Application.Invoke(delegate
            {
                //Chat chat = new Chat(src, dest);
                Chat chat = new Chat(src, dest);
                chatWindows.Add(dest.Username, chat);
                chat.Show();
            }); 
        }

        public void RequestRefused(User src, User dest)
        {
            usersWindow.RemoveRequested(dest);
        }

        public void MessageReceived(Message msg)
        {
            chatWindows[msg.src.Username].AddMessage(msg);
        }

        public void LeaveChat(User u)
        {
            Console.WriteLine("[Leave Chat] {0}", u.Username);
            Gtk.Application.Invoke(delegate
            {
                chatWindows[u.Username].Destroy();
                chatWindows.Remove(u.Username);
            });
        }

        public void RemoveOfflineChats(List<User> users)
        {
            foreach (KeyValuePair<string, Chat> pair in chatWindows)
            {
                User u = users.Find(e => e.Username == pair.Key);
                if (u == null)
                {
                    LeaveChat(new User(pair.Key, ""));
                }
            }

        }

        public Dictionary<string, Chat> GetChats()
        {
            return chatWindows;
        }
    }
}
