using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class DtoA : BaseTransition<StateD, StateA>, IAppTransition
    {
        public DtoA(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}