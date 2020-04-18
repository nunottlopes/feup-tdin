using System;
using Common.Authentication;

namespace Common.Messages
{
    public interface IChat
    {
        void Send(Message message);
        void Exit(Guid guid, User src);
        void RequestMade(Guid guid, User u);
    }
}
