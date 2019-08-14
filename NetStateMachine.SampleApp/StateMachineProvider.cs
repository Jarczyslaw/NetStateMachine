using NetStateMachine.SampleApp.States;
using NetStateMachine.SampleApp.Transistions;
using Unity;

namespace NetStateMachine.SampleApp
{
    public class StateMachineProvider : IStateMachineProvider
    {
        private readonly IMessageBroker messageBroker;

        public StateMachineProvider(IMessageBroker messageBroker)
        {
            this.messageBroker = messageBroker;
        }

        public StateMachine GetStateMachine()
        {
            var stateMachine = new StateMachine();

            stateMachine.AddState(new StateA(messageBroker))
                .AddState(new StateB(messageBroker))
                .AddState(new StateC(messageBroker))
                .AddState(new StateD(messageBroker))
                .AddState(new StateX(messageBroker));

            stateMachine.AddTransition(new AtoB(messageBroker))
                .AddTransition(new AtoC(messageBroker))
                .AddTransition(new BtoC(messageBroker))
                .AddTransition(new BtoD(messageBroker))
                .AddTransition(new BtoB(messageBroker))
                .AddTransition(new CtoD(messageBroker))
                .AddTransition(new DtoX(messageBroker))
                .AddTransition(new CtoA(messageBroker));

            return stateMachine;
        }
    }
}