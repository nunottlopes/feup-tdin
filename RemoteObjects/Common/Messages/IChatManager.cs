using System;
using System.Collections.Generic;
using Common.Authentication;

namespace Common.Messages
{
    public interface IChatManager
    {
        void AddChat(Guid guid, List<User> users);
        void AddUserToChat(Guid guid, User user);
        void RemoveUserFromChat(Guid guid, User user);
        List<User> GetUsersInChat(Guid guid);
        void RemoveUser(User user);
        void AddRequest(Guid guid, User user);
        List<User> GetRequests(Guid guid);
        void RemoveRequest(Guid guid, User user);
        bool ChatExists(Guid guid);
    }
}
