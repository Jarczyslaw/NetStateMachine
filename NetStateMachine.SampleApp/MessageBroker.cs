using System;

namespace NetStateMachine.SampleApp
{
    public class MessageBroker : IMessageBroker
    {
        public Action<string> OnSend { get; set; }

        public void SendMessage(string message)
        {
            OnSend?.Invoke(message);
        }
    }
}