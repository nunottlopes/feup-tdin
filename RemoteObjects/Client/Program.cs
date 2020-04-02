using System;
using Common;
using System.Runtime.Remoting;
using Gtk;
using Client.Windows;

namespace Client
{
    class Client
    {
        public static void Main(string[] args)
        {
            Application.Init();
            Auth win = new Auth();
            win.Title = "TDIN | Authentication";
            win.Show();

            Application.Run();
        }
    }

    //class Client
    //{
    //    static void Main(string[] args)
    //    {
    //        int v = 7;

    //        RemotingConfiguration.Configure("Client.exe.config", false);
    //        IRemote obj = (IRemote)RemotingServices.Connect(typeof(IRemote), "tcp://localhost:9000/Server/RemObj");
    //        Console.WriteLine(obj.Hello());
    //        Console.ReadLine();
    //        Console.WriteLine(obj.Modify(ref v));
    //        Console.WriteLine("Client after: {0}", v);
    //    }
    //}
}
