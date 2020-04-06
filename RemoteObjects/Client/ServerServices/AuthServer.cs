using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using Common.Authentication;

namespace Client.ServerServices
{
    public class AuthServer : IAuthentication
    {
        private IAuthentication auth;

        public AuthServer()
        {
            auth = (IAuthentication)RemotingServices.Connect(typeof(IAuthentication), "tcp://localhost:9000/Server/Auth");
        }

        public User Login(string username, string password)
        {
            return auth.Login(username, password);
        }

        public bool Register(string username, string name, string password)
        {
            return auth.Register(username, name, password);
        }

        public bool Logout(string username)
        {
            return auth.Logout(username);
        }

        public List<User> GetOnline()
        {
            return auth.GetOnline();
        }
    }
}
