using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Server.Database
{
    public static class DBManager
    {
        private static string FILENAME = "users.xml";

        private static List<User> Users;

        public static void SaveFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("users");
            doc.AppendChild(rootNode);

            foreach(User user in Users)
            {
                XmlNode userNode = user.ToXml(doc);
                rootNode.AppendChild(userNode);
            }

            doc.Save(FILENAME);
        }

        public static void LoadFile()
        {
            if (!File.Exists(FILENAME))
                Users = new List<User>();

            XmlDocument doc = new XmlDocument();
            doc.Load(FILENAME);

            List<User> temp = new List<User>();

            XmlNodeList userNodes = doc.SelectNodes("//users/user");
            foreach(XmlNode userNode in userNodes)
            {
                User user = new User(userNode);
                temp.Add(user);
            }

            Users = temp;
        }
    }
}
