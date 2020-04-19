using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Common.Authentication;
using Common.Messages;

namespace Server.Services
{
    public class ChatManager : MarshalByRefObject, IChatManager
    {
        private ConcurrentDictionary<Guid, HashSet<User>> chats;
        private ConcurrentDictionary<Guid, HashSet<User>> requests;

        public ChatManager()
        {
            chats = new ConcurrentDictionary<Guid, HashSet<User>>();
            requests = new ConcurrentDictionary<Guid, HashSet<User>>();
        }

        public void AddChat(Guid guid, List<User> users)
        {
            chats.TryAdd(guid, new HashSet<User>(users));
            requests.TryAdd(guid, new HashSet<User>());
            Console.WriteLine("[Chat Manager] New Chat {0}", guid);
            Console.WriteLine("[Chat Manager] Current no of chats: {0}", chats.Count);
        }

        public void AddUserToChat(Guid guid, User user)
        {
            chats[guid].Add(user);
            Console.WriteLine("[Chat Manager] Add User {0} to {1}", user.Username, guid);
        }

        public List<User> GetUsersInChat(Guid guid)
        {
            return chats[guid].ToList();
        }

        public void RemoveUserFromChat(Guid guid, User user)
        {
            if (!chats[guid].Contains(user)) return;

            chats[guid].Remove(user);
            Console.WriteLine("[Chat Manager] Remove User {0} from {1}", user.Username, guid);
            if(chats[guid].Count == 0)
            {
                chats.TryRemove(guid, out _);
                requests.TryRemove(guid, out _);
                Console.WriteLine("[Chat Manager] Remove Chat {0}", guid);
                Console.WriteLine("[Chat Manager] Current no of chats: {0}", chats.Count);
            }
        }

        public void RemoveUser(User user)
        {
            List<Guid> guids = new List<Guid>();
            foreach(KeyValuePair<Guid, HashSet<User>> kv in chats)
            {
                if(kv.Value.Contains(user))
                    guids.Add(kv.Key);
            }

            foreach(Guid guid in guids)
            {
                RemoveUserFromChat(guid, user);
            }
        }

        public void AddRequest(Guid guid, User user)
        {
            requests[guid].Add(user);
            Console.WriteLine("[Chat Manager] Add Request to User {0} in {1}", user.Username, guid);
        }

        public void RemoveRequest(Guid guid, User user)
        {
            requests[guid].Remove(user);
            Console.WriteLine("[Chat Manager] Remove Request to User {0} in {1}", user.Username, guid);
        }

        public List<User> GetRequests(Guid guid)
        {
            return requests[guid].ToList();
        }

        public bool ChatExists(Guid guid)
        {
            return requests.ContainsKey(guid);
        }
    }
}
