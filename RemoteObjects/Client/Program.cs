using System;
using System.Runtime.Remoting;
using Gtk;
using Client.Windows;

namespace Client
{
    class Client
    {
        public static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Client.exe.config", false);
            Console.WriteLine("[Client] Started");

            Application.Init();

            WindowManager.getInstance();

            Application.Run();
        }
    }
}
