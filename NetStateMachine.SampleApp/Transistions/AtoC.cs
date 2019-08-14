using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class AtoC : BaseTransition<StateA, StateC>
    {
        public AtoC(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}