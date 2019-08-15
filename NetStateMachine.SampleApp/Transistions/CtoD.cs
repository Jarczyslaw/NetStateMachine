using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class CtoD : BaseTransition<StateC, StateD>, IAppTransition
    {
        public CtoD(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}