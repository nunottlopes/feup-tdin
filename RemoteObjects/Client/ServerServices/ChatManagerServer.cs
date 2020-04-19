using System;
using System.Collections.Generic;
using Client.Utils;
using Common.Authentication;
using Common.Messages;

namespace Client.ServerServices
{
    public class ChatManagerServer : IChatManager
    {
        private IChatManager cm;

        public ChatManagerServer()
        {
            try
            {
                cm = (IChatManager)RemoteNew.New(typeof(IChatManager));
            }
            catch (Exception)
            {
                cm = null;
            }
        }

        public void AddChat(Guid guid, List<User> users)
        {
            cm?.AddChat(guid, users);
        }

        public void AddUserToChat(Guid guid, User user)
        {
            cm?.AddUserToChat(guid, user);
        }

        public List<User> GetUsersInChat(Guid guid)
        {
            return cm?.GetUsersInChat(guid);
        }

        public void RemoveUserFromChat(Guid guid, User user)
        {
            cm?.RemoveUserFromChat(guid, user);
        }

        public void RemoveUser(User user)
        {
            cm?.RemoveUser(user);
        }

        public void AddRequest(Guid guid, User user)
        {
            cm?.AddRequest(guid, user);
        }

        public void RemoveRequest(Guid guid, User user)
        {
            cm?.RemoveRequest(guid, user);
        }

        public List<User> GetRequests(Guid guid)
        {
            return cm.GetRequests(guid);
        }

        public bool ChatExists(Guid guid)
        {
            return cm.ChatExists(guid);
        }
    }
}
