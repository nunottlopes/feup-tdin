using System;
using System.Runtime.Remoting;
using Server.Database;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Server.exe.config", false);

            DBManager.LoadFile();

            Console.WriteLine("Press return to exit");
            Console.ReadLine();
        }
    }
}
