using System;
using Client.Windows;
using Common.Authentication;
using Common.Messages;

namespace Client.Services
{
    public class Request : MarshalByRefObject, IRequest
    {
        public void MakeRequest(User src, User dest, IRequestCallback callback)
        {
            Console.WriteLine("[Request Received] {0}", src.Username);
            WindowManager.getInstance().RequestReceived(src, callback);
        }
    }

    public class RequestCallback : MarshalByRefObject, IRequestCallback
    {
        public void Accepted(User user)
        {
            Console.WriteLine("[Request Accepted] {0}", user.Username);
            WindowManager.getInstance().RequestAccepted(user);
        }

        public void Refused(User user)
        {
            Console.WriteLine("[Request Refused] {0}", user.Username);
            WindowManager.getInstance().RequestRefused(user);
        }
    }
}
