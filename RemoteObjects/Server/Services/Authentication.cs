using System;
using Common.Authentication;
using Server.Database;

namespace Server.Services
{
    public class Authentication : MarshalByRefObject, IAuthentication
    {
        public bool Login(string username, string password)
        {
            Console.WriteLine("[Login] {0}:{1}", username, password);
            if (!DBManager.hasUsername(username))
            {
                Console.WriteLine("[Login] User {0} doesn't exist", username);
                return false;
            }

            User user = DBManager.Users.Find(obj => obj.Equals(new User(username)));

            if(!user.Password.Equals(password))
            {
                Console.WriteLine("[Login] User {0} wrong password", username);
                return false;
            }
            

            Console.WriteLine("[Login] User {0} successfully logged in", username);
            return true;
        }

        public bool Logout(string username)
        {
            throw new NotImplementedException();
        }

        public bool Register(string username, string name, string password)
        {
            Console.WriteLine("[Register] {0}:{1} ({2})", username, password, name);
            if (DBManager.hasUsername(username))
            {
                Console.WriteLine("[Register] User {0} already exists", username);
                return false;
            }

            DBManager.Users.Add(new User(username, password, name));
            Console.WriteLine("[Register] User {0} successfully registered", username);
            return true;
        }
    }
}
