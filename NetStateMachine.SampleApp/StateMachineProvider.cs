using NetStateMachine.SampleApp.States;

namespace NetStateMachine.SampleApp
{
    public class StateMachineProvider : IStateMachineProvider
    {
        public StateMachine GetStateMachine()
        {
            var stateMachine = new StateMachine();
            stateMachine.AddState(new StateA());
            return stateMachine;
        }
    }
}