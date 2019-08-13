namespace NetStateMachine.SampleApp.States
{
    public class StateA : BaseState
    {
        public StateA(IMessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateA);
        }
    }
}