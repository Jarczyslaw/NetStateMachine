using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class DtoX : BaseTransition<StateD, StateX>
    {
        public DtoX(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}