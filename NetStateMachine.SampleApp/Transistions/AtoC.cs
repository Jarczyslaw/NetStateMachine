using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp.Transistions
{
    public class AtoC : BaseTransition<StateA, StateC>, IAppTransition
    {
        public AtoC(IMessageBroker messageBroker) : base(messageBroker)
        {
        }
    }
}