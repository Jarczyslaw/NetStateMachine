namespace NetStateMachine.SampleApp.States
{
    public class StateD : BaseState
    {
        public StateD(IMessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateD);
        }
    }
}