﻿using System;
using System.Runtime.Remoting;
using Common.Authentication;

namespace Client.ServerServices
{
    public class AuthServer
    {
        private IAuthentication auth;

        public AuthServer()
        {
            auth = (IAuthentication)RemotingServices.Connect(typeof(IAuthentication), "tcp://localhost:9000/Server/Auth");
        }

        public bool Login(string username, string password)
        {
            return auth.Login(username, password);
        }

        public bool Register(string username, string name, string password)
        {
            return auth.Register(username, name, password);
        }

        public void Logout(string username)
        {
            throw new NotImplementedException();
        }
    }
}
