﻿using System;

namespace Common.Authentication
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }

        public User(string username, string name, int port, string address)
        {
            this.Username = username;
            this.Name = name;
            this.Port = port;
            this.Address = address;
        }

        public User(string username, string name)
        {
            this.Username = username;
            this.Name = name;
            this.Port = 0;
            this.Address = "";
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Username, this.Name);
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                User u = (User)obj;
                return this.Username.Equals(u.Username);
            }
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }
    }
}
