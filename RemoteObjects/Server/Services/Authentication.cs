using System;
using System.Collections.Generic;
using System.Threading;
using Common.Authentication;
using Server.Database;

namespace Server.Services
{
    public class Authentication : MarshalByRefObject, IAuthentication
    {
        public event OnlineHandler OnlineChanged;

        public User Login(string username, string password, int port, string address)
        {
            Console.WriteLine("[Login] {0}:{1}, address: {2}", username, password, address);
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
            user.Port = port;
            user.Address = address;
            Console.WriteLine("[Login] User {0} successfully logged in", username);

            OnLoginChange();

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

            OnLoginChange();

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

        private void OnLoginChange()
        {
            if (OnlineChanged != null)
            {
                Delegate[] invkList = OnlineChanged.GetInvocationList();
                Console.WriteLine("[Authentication] Notifying {0} Clients", invkList.Length);

                foreach (OnlineHandler handler in invkList)
                {
                    new Thread(() =>
                    {
                        try
                        {
                            handler(GetOnline());
                        }
                        catch (Exception)
                        {
                            OnlineChanged -= handler;
                        }
                    }).Start();
                }
            }
        }
    }
}
