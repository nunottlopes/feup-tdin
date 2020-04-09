using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Client.Utils;
using Common.Authentication;

namespace Client.ServerServices
{
    public class AuthServer : IAuthentication
    {
        private readonly IAuthentication Auth;
        private readonly int Port;

        private OnlineHandlerRepeater UsersOnlineRepeater;
        public event OnlineHandler OnlineChanged;

        private OnlineHandler t1;
        private OnlineHandler t2;

        public AuthServer()
        {
            TcpChannel chan = (TcpChannel)ChannelServices.GetChannel("tcp");
            ChannelDataStore data = (ChannelDataStore)chan.ChannelData;
            Port = new Uri(data.ChannelUris[0]).Port;

            try
            {
                Auth = (IAuthentication)RemoteNew.New(typeof(IAuthentication));
                UsersOnlineRepeater = new OnlineHandlerRepeater();
            } catch (Exception)
            {
                Auth = null;
            }
        }

        public User Login(string username, string password, int port = 0)
        {
            if (Auth == null) return null;
            return Auth.Login(username, password, this.Port);
        }

        public bool Register(string username, string name, string password)
        {
            if (Auth == null) return false;
            return Auth.Register(username, name, password);
        }

        public bool Logout(string username)
        {
            if (Auth == null) return false;
            if(t1 != null)
                UsersOnlineRepeater.OnlineChanged -= t1;
            if (t1 != null)
                Auth.OnlineChanged -= t2;
            return Auth.Logout(username);
        }

        public List<User> GetOnline()
        {
            if (Auth == null) return null;
            return Auth.GetOnline();
        }

        public void AddOnlineHandler(OnlineHandler h)
        {
            if (Auth == null) return;
            t1 = new OnlineHandler(h);
            UsersOnlineRepeater.OnlineChanged += t1;
            t2 = new OnlineHandler(UsersOnlineRepeater.Repeater);
            Auth.OnlineChanged += t2;
        }
    }
}
