using System;

namespace NetStateMachine.SampleApp
{
    public interface IMessageBroker
    {
        Action<string> OnSend { get; set; }

        void SendMessage(string message);
    }
}