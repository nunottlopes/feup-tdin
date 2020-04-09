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
            Console.WriteLine("[Request Received] {0}", src);
            WindowManager.getInstance().RequestReceived(src, callback);
        }
    }

    public class RequestCallback : MarshalByRefObject, IRequestCallback
    {
        public void Accepted()
        {
            Console.WriteLine("Accepted");
        }

        public void Refused()
        {
            Console.WriteLine("Refused");
        }
    }
}
