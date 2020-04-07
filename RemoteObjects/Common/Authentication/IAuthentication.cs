using System;
using System.Collections.Generic;

namespace Common.Authentication
{
    public delegate void OnlineHandler(List<User> users);

    public interface IAuthentication
    {
        event OnlineHandler OnlineChanged;

        bool Register(string username, string name, string password);
        User Login(string username, string password, int Port);
        bool Logout(string username);
        List<User> GetOnline();
    }

    public class OnlineHandlerRepeater : MarshalByRefObject
    {
        public event OnlineHandler OnlineChanged;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(List<User> users)
        {
            OnlineChanged?.Invoke(users);
        }
    }
}
