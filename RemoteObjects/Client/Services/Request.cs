using System;
using System.Collections.Generic;
using Client.Windows;
using Common.Authentication;
using Common.Messages;

namespace Client.Services
{
    public class Request : MarshalByRefObject, IRequest
    {
        public void MakeRequest(User src, User dest, IRequestCallback callback)
        {
            Console.WriteLine("[Request Received]");
            WindowManager.getInstance().RequestReceived(src, callback);
        }
    }

    public class RequestCallback : MarshalByRefObject, IRequestCallback
    {
        public void Accepted(Guid guid, User src, User dest)
        {
            Console.WriteLine("[Request Accepted] {0}", dest.Username);
            WindowManager.getInstance().RequestAccepted(guid, src, dest);
        }

        public void Refused(User src, User dest)
        {
            Console.WriteLine("[Request Refused] {0}", dest.Username);
            WindowManager.getInstance().RequestRefused(src, dest);
        }
    }
}
