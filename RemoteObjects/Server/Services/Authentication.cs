using System;
using System.Collections.Generic;
using Common.Authentication;
using Server.Database;

namespace Server.Services
{
    public class Authentication : MarshalByRefObject, IAuthentication
    {
        public User Login(string username, string password)
        {
            Console.WriteLine("[Login] {0}:{1}", username, password);
            if (!DBManager.hasUsername(username))
            {
                Console.WriteLine("[Login] User {0} doesn't exist", username);
                return null;
            }

            UserServer user = DBManager.GetUser(username);

            if(!user.Password.Equals(password))
            {
                Console.WriteLine("[Login] User {0} wrong password", username);
                return null;
            }

            if(user.Online)
            {
                Console.WriteLine("[Login] User {0} is already online", username);
                return null;
            }

            user.Online = true;
            Console.WriteLine("[Login] User {0} successfully logged in", username);
            return user.GetUser();
        }

        public bool Logout(string username)
        {
            UserServer user = DBManager.GetUser(username);

            if(!user.Online)
            {
                Console.WriteLine("[Logout] User {0} not logged in", username);
                return false;
            }

            user.Online = false;
            Console.WriteLine("[Logout] User {0} successfully logged out", username);
            return true;
        }

        public bool Register(string username, string name, string password)
        {
            Console.WriteLine("[Register] {0}:{1} ({2})", username, password, name);
            if (DBManager.hasUsername(username))
            {
                Console.WriteLine("[Register] User {0} already exists", username);
                return false;
            }

            DBManager.Users.Add(new UserServer(username, password, name));
            Console.WriteLine("[Register] User {0} successfully registered", username);
            return true;
        }

        public List<User> GetOnline()
        {
            List<UserServer> online = DBManager.getOnline();
            List<User> ret = new List<User>();
            foreach(UserServer u in online)
            {
                ret.Add(u.GetUser());
            }

            return ret;
        }
    }
}
