using System;
using System.Collections.Generic;
using Common.Authentication;

namespace Common.Messages
{
    public interface IRequest
    {
        void MakeRequest(User src, User dest, IRequestCallback callback);
    }

    public interface IRequestCallback
    {
        void Accepted(Guid guid, User src, User dest);
        void Refused(User src, User dest);
    }
}
