using System;
namespace Client.Windows
{
    public class WindowManager
    {
        private static WindowManager instance;

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
            authWindow = new Auth();
            authWindow.Show();
        }

        public void Register()
        {
            authWindow.Hide();
            registerWindow = new Register();
            registerWindow.Show();
        }
    }
}
