using System;
using Common.Authentication;

namespace Common.Messages
{
    public interface IGroupRequest
    {
        void MakeRequest(Guid guid, User src, User dest);
        void GroupRequestRefused(Guid guid, User src);
        void GroupRequestAccepted(Guid guid, User src);
    }
}
