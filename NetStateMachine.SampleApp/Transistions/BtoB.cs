using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class BtoB : BaseTransition<StateB, StateB>, IAppTransition
    {
        public BtoB(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}