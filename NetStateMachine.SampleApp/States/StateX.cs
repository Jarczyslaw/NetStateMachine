namespace NetStateMachine.SampleApp.States
{
    public class StateX : BaseState
    {
        public StateX(IMessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateX);
        }
    }
}