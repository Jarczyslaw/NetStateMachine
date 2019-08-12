namespace NetStateMachine.SampleApp.States
{
    public class StateB : BaseState
    {
        public StateB(MessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateB);
        }
    }
}