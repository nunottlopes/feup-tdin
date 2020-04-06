using System;
using System.Collections.Generic;

namespace Common.Authentication
{
    public interface IAuthentication
    {
        bool Register(string username, string name, string password);
        User Login(string username, string password);
        bool Logout(string username);
        List<User> GetOnline();
    }
}
