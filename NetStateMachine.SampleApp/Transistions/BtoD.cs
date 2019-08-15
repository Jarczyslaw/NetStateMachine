using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class BtoD : BaseTransition<StateB, StateD>, IAppTransition
    {
        public BtoD(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}