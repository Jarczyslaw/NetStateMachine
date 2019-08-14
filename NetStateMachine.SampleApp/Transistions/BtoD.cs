using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class BtoD : BaseTransition<StateB, StateD>
    {
        public BtoD(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}