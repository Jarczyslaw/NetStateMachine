namespace NetStateMachine.SampleApp.States
{
    public class StateB : BaseState
    {
        public StateB(IMessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateB);
        }
    }
}