using System;
using Common.Authentication;
using System.Runtime.Remoting;
using Gtk;
using Client.Windows;

namespace Client
{
    //class Client
    //{
    //    public static void Main(string[] args)
    //    {
    //        Application.Init();
    //        Auth win = new Auth();
    //        win.Title = "TDIN | Authentication";
    //        win.Show();

    //        Application.Run();
    //    }
    //}

    class Client
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Client.exe.config", false);

            IAuthentication auth = (IAuthentication)RemotingServices.Connect(typeof(IAuthentication), "tcp://localhost:9000/Server/Auth");

            Console.WriteLine("L - {0}:{1} - {2}", "amadeu", "12345", auth.Login("amadeu", "12345"));
            Console.WriteLine("R - {0}:{1} - {2}", "amadeu", "12345", auth.Register("amadeu", "amadeupereira", "12345"));
            Console.WriteLine("L - {0}:{1} - {2}", "amadeu", "12345", auth.Login("amadeu", "12345"));
            Console.WriteLine("R - {0}:{1} - {2}", "amadeu", "12345", auth.Register("amadeu", "nome", "12345"));
            Console.WriteLine("R - {0}:{1} - {2}", "nuno", "12345", auth.Register("nuno", "nunito", "12345"));
            Console.WriteLine("L - {0}:{1} - {2}", "nuno", "12345", auth.Login("nuno", "12345"));
        }
    }
}
