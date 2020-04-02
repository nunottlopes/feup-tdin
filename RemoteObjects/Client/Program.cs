using System;
using Common;
using System.Runtime.Remoting;
using Gtk;

namespace Client
{
    //class MainClass
    //{
    //    public static void Main(string[] args)
    //    {
    //        Application.Init();
    //        MainWindow win = new MainWindow();
    //        win.Title = "Client";
    //        win.Show();

    //        ChatWindow chat = new ChatWindow();
    //        chat.Title = "Chat with Amadeu";
    //        chat.Show();

    //        Application.Run();
    //    }
    //}

    class Client
    {
        static void Main(string[] args)
        {
            int v = 7;

            RemotingConfiguration.Configure("Client.exe.config", false);
            IRemote obj = (IRemote)RemotingServices.Connect(typeof(IRemote), "tcp://localhost:9000/Server/RemObj");
            Console.WriteLine(obj.Hello());
            Console.ReadLine();
            Console.WriteLine(obj.Modify(ref v));
            Console.WriteLine("Client after: {0}", v);
        }
    }
}
