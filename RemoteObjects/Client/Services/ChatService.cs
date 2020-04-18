using System;
using Client.Windows;
using Common.Authentication;
using Common.Messages;

namespace Client.Services
{
    public class ChatService : MarshalByRefObject, IChat
    {
        public void Send(Message message)
        {
            Console.WriteLine("[Message] {1}", message.src.Username, message.content);
            WindowManager.getInstance().MessageReceived(message);
        }

        public void Exit(User src)
        {
            WindowManager.getInstance().LeaveChat(src);
        }
    }
}
