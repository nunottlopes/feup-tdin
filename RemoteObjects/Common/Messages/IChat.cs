using System;
namespace Common.Messages
{
    public interface IChat
    {
        void Send(Message message);
    }
}
