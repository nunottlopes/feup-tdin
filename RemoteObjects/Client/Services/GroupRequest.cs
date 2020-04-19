using System;
using Client.Windows;
using Common.Authentication;
using Common.Messages;

namespace Client.Services
{
    public class GroupRequest : MarshalByRefObject, IGroupRequest
    {
        public void MakeRequest(Guid guid, User src, User dest)
        {
            Console.WriteLine("Group request received from {0}", src);
            WindowManager.getInstance().AddGroupChatRequest(guid, src, dest);
        }

        public void GroupRequestRefused(Guid guid, User src)
        {
            WindowManager.getInstance().UpdateChat(guid);
        }

        public void GroupRequestAccepted(Guid guid, User src)
        {
            WindowManager.getInstance().UpdateChat(guid);
        }
    }
}
