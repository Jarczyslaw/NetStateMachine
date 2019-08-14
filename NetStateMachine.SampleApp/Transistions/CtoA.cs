using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class CtoA : BaseTransition<StateC, StateA>
    {
        public CtoA(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}