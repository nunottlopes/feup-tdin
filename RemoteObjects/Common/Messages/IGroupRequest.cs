using System;
using System.Collections.Generic;
using Common.Authentication;

namespace Common.Messages
{
    public interface IGroupRequest
    {
        void MakeRequest(Guid guid, List<User> src, User dest);
    }
}
