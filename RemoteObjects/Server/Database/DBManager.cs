using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Server.Database
{
    public static class DBManager
    {
        private static string FILENAME = "users.xml";

        public static List<User> Users { get; set; }

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
            Console.WriteLine("[DBManager] Users saved");
        }

        public static bool hasUsername(string username)
        {
            User temp = new User(username);
            return Users.Contains(temp);
        }

        public static void LoadFile()
        {
            if (!File.Exists(FILENAME))
            {
                Users = new List<User>();
                Console.WriteLine("[DBManager] No users file found. Default action");
                return;
            }

            Console.WriteLine("[DBManager] Loading users...");

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
            Console.WriteLine("[DBManager] Users loaded");
        }
    }
}
