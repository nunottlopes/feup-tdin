using System;
using Gtk;

namespace Client
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Title = "Client";
            win.Show();

            ChatWindow chat = new ChatWindow();
            chat.Title = "Chat with Amadeu";
            chat.Show();

            Application.Run();
        }
    }
}
