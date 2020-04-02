using System;
using System.Runtime.Remoting;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Server.exe.config", false);
            Console.WriteLine("Press return to exit ----");
            Console.ReadLine();
        }
    }
}
