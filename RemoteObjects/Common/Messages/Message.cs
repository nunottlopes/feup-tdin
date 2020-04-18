using System;
using Common.Authentication;

namespace Common.Messages
{
    [Serializable]
    public class Message
    {
        public Guid guid { get; set; }
        public User src { get; set; }
        public string content {get; set; }

        public Message(Guid guid, User src, string content)
        {
            this.guid = guid;
            this.src = src;
            this.content = content;
        }
    }
}
