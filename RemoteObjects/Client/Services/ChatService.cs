using System;
using Common.Messages;

namespace Client.Services
{
    public class ChatService : MarshalByRefObject, IChat
    {
        public void Send(Message message)
        {
            Console.WriteLine("[Message from {0}] {1}", message.src.Username, message.content);
        }

        public void Exit()
        {
            Console.WriteLine("[Chat] Exit");
        }
    }
}
