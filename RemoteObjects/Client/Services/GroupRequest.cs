using System;
using System.Collections.Generic;
using Common.Authentication;
using Common.Messages;

namespace Client.Services
{
    public class GroupRequest : MarshalByRefObject, IGroupRequest
    {
        public void MakeRequest(Guid guid, List<User> src, User dest)
        {
            Console.WriteLine("Group request received");
        }
    }
}
