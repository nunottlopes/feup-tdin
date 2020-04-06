using System;
using System.Xml;
using Common.Authentication;

namespace Server.Database
{
    public class UserServer : User
    {
        static int MaxId = 0;

        public int Id { get; set; }
        //public string Username { get; set; }
        public string Password { get; set; }
        //public string Name { get; set; }
        public bool Login { get; set; }

        public UserServer(string username, string password, string name) :
            base(username, name)
        {
            this.Id = ++MaxId;
            this.Password = password;
            this.Login = false;
        }

        public UserServer(XmlNode xmlNode) :
            base("", "")
        {
            this.Id = int.Parse(xmlNode.Attributes["id"].Value);
            if (this.Id > MaxId) MaxId = this.Id; 
            this.Username = xmlNode.SelectSingleNode("username").InnerText;
            this.Password = xmlNode.SelectSingleNode("password").InnerText;
            this.Name = xmlNode.SelectSingleNode("name").InnerText;
            this.Login = false;
        }

        // Constructor for testing purposes
        public UserServer(string username) :
            base(username, "")
        {
            this.Id = -1;
            this.Password = null;
            this.Name = null;
            this.Login = false;
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode node = doc.CreateElement("user");

            XmlAttribute attribute = doc.CreateAttribute("id");
            attribute.Value = Id.ToString();
            node.Attributes.Append(attribute);

            XmlNode username = doc.CreateElement("username");
            username.InnerText = Username;
            node.AppendChild(username);

            XmlNode password = doc.CreateElement("password");
            password.InnerText = Password;
            node.AppendChild(password);

            XmlNode name = doc.CreateElement("name");
            name.InnerText = Name;
            node.AppendChild(name);

            return node;
        }

        public User GetUser()
        {
            return new User(this.Username, this.Password);
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

        public bool Equals(User other)
        {
            if (other == null) return false;
            return (this.Username.Equals(other.Username));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}:{2} ({3})", this.Id, this.Username, this.Password, this.Name);
        }
    }
}
