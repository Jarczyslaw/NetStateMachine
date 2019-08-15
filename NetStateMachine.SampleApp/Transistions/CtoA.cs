using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class CtoA : BaseTransition<StateC, StateA>, IAppTransition
    {
        public CtoA(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}