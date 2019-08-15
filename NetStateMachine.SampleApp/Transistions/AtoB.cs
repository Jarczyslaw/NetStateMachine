using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class AtoB : BaseTransition<StateA, StateB>, IAppTransition
    {
        public AtoB(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}