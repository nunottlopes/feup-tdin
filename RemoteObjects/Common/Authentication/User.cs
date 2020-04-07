using System;

namespace Common.Authentication
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }

        public User(string username, string name, int port)
        {
            this.Username = username;
            this.Name = name;
            this.Port = port;
        }

        public User(string username, string name)
        {
            this.Username = username;
            this.Name = name;
            this.Port = 0;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Username, this.Name);
        }
    }
}
