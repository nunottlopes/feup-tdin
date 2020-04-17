using System;
using Common.Authentication;

namespace Common.Messages
{
    [Serializable]
    public class Message
    {
        public User src { get; set; }
        public User dest { get; set; }
        public string content {get; set; }

        public Message(User src, User dest, string content)
        {
            this.src = src;
            this.dest = dest;
            this.content = content;
        }
    }
}
