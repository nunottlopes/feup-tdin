using System;
using Common.Authentication;

namespace Common.Messages
{
    public interface IRequest
    {
        void MakeRequest(User src, User dest, IRequestCallback callback);
    }

    public interface IRequestCallback
    {
        void Accepted(User user);
        void Refused(User user);
    }
}
