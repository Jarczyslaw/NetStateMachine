using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class AtoB : BaseTransition<StateA, StateB>
    {
        public AtoB(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}