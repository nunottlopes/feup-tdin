using System;
namespace Client.Windows
{
    public class WindowManager
    {
        private static WindowManager instance;
        private enum State { LOGIN, REGISTER };
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
    }
}
