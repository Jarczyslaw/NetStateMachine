using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class BtoC : BaseTransition<StateB, StateC>, IAppTransition
    {
        public BtoC(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}