namespace NetStateMachine.SampleApp.States
{
    public class StateX : BaseState
    {
        public StateX(MessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateX);
        }
    }
}