using System;
using System.Xml;

namespace Server.Database
{
    public class User
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string Name { get; set; }

        public User(int id, string username, string password, string name)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Name = name;
        }

        public User(XmlNode xmlNode)
        {
            this.Id = int.Parse(xmlNode.Attributes["id"].Value);
            this.Username = xmlNode.SelectSingleNode("username").InnerText;
            this.Password = xmlNode.SelectSingleNode("password").InnerText;
            this.Name = xmlNode.SelectSingleNode("name").InnerText;
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
    }
}
