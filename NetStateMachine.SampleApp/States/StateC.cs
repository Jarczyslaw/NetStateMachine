namespace NetStateMachine.SampleApp.States
{
    public class StateC : BaseState
    {
        public StateC(MessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateC);
        }
    }
}