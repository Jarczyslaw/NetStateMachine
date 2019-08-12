namespace NetStateMachine.SampleApp.States
{
    public class StateA : BaseState
    {
        public StateA(MessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateA);
        }
    }
}