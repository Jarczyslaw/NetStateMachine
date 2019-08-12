using System;

namespace NetStateMachine.SampleApp
{
    public class MessageBroker
    {
        public Action<string> OnSend;

        public void SendMessage(string message)
        {
            OnSend?.Invoke(message);
        }
    }
}