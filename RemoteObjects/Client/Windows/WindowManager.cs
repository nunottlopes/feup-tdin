﻿using System;
using Common.Authentication;

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
                usersWindow = new Users(user);
                usersWindow.Show();
                state = State.USERS;
            }
        }
    }
}