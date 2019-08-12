namespace NetStateMachine.SampleApp.States
{
    public class StateD : BaseState
    {
        public StateD(MessageBroker messageBroker)
            : base(messageBroker)
        {
            Name = nameof(StateD);
        }
    }
}