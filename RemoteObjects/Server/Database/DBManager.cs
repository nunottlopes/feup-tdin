using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Server.Database
{
    public static class DBManager
    {
        private static string FILENAME = "users.xml";

        public static List<UserServer> Users { get; set; }

        public static bool hasUsername(string username)
        {
            UserServer temp = new UserServer(username);
            return Users.Contains(temp);
        }

        public static UserServer GetUser(string username)
        {
            UserServer temp = new UserServer(username);
            return Users.Find(u => u.Equals(temp));
        }

        public static void SaveFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("users");
            doc.AppendChild(rootNode);

            foreach(UserServer user in Users)
            {
                XmlNode userNode = user.ToXml(doc);
                rootNode.AppendChild(userNode);
            }

            doc.Save(FILENAME);
            Console.WriteLine("[DBManager] Users saved");
        }

        public static void LoadFile()
        {
            if (!File.Exists(FILENAME))
            {
                Users = new List<UserServer>();
                Console.WriteLine("[DBManager] No users file found. Default action");
                return;
            }

            Console.WriteLine("[DBManager] Loading users...");

            XmlDocument doc = new XmlDocument();
            doc.Load(FILENAME);

            List<UserServer> temp = new List<UserServer>();

            XmlNodeList userNodes = doc.SelectNodes("//users/user");
            foreach(XmlNode userNode in userNodes)
            {
                UserServer user = new UserServer(userNode);
                temp.Add(user);
            }

            Users = temp;
            Console.WriteLine("[DBManager] Users loaded");
        }
    }
}
