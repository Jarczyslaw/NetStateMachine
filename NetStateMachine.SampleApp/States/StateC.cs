namespace NetStateMachine.SampleApp.States
{
    public class StateC : BaseState
    {
        public StateC(IMessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateC);
        }
    }
}