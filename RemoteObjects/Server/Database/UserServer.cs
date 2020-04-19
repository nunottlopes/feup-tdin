using System;
using System.Xml;
using Common.Authentication;

namespace Server.Database
{
    public class UserServer : User
    {
        static int MaxId = 0;

        // Username, Name, Port from User
        public int Id { get; set; }
        public string Password { get; set; }
        public bool Online { get; set; }

        public UserServer(string username, string password, string name) :
            base(username, name)
        {
            this.Id = ++MaxId;
            this.Password = password;
            this.Online = false;
            this.Port = -1;
            this.Address = "";
        }

        public UserServer(XmlNode xmlNode) :
            base("", "")
        {
            this.Id = int.Parse(xmlNode.Attributes["id"].Value);
            if (this.Id > MaxId) MaxId = this.Id; 
            this.Username = xmlNode.SelectSingleNode("username").InnerText;
            this.Password = xmlNode.SelectSingleNode("password").InnerText;
            this.Name = xmlNode.SelectSingleNode("name").InnerText;
            this.Online = false;
            this.Port = -1;
            this.Address = "";
        }

        // Constructor for testing purposes
        public UserServer(string username) :
            base(username, "")
        {
            this.Id = -1;
            this.Password = null;
            this.Name = null;
            this.Online = false;
            this.Port = -1;
            this.Address = "";
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
            return new User(this.Username, this.Name, this.Port, this.Address);
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}:{2}, address:{3} {4} ({5})", this.Id, this.Username, this.Password, this.Address, this.Name, this.Online);
        }
    }
}
