using System;
using Common.Authentication;

namespace Common.Messages
{
    [Serializable]
    public class Message
    {
        public Guid guid { get; set; }
        public User src { get; set; }
        public string content { get; set; }
        public string fileName { get; set; }
        public byte[] file { get; set; }
        public enum Type { TEXT, FILE }
        public Type messageType { get; set; }

        public Message(Guid guid, User src, string content, Message.Type type)
        {
            this.guid = guid;
            this.src = src;
            this.content = content;
            this.messageType = type;
        }

        public Message(Guid guid, User src, string fileName, byte[] file, Message.Type type)
        {
            this.guid = guid;
            this.src = src;
            this.file = file;
            this.fileName = fileName;
            this.messageType = type;
        }
    }
}
