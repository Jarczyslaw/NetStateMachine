using NetStateMachine.Data;

namespace NetStateMachine.SampleApp.States
{
    public class BaseState : State
    {
        private readonly MessageBroker messageBroker;

        public BaseState(MessageBroker messageBroker)
        {
            this.messageBroker = messageBroker;
        }

        public override void OnEnter(OnEnterData data)
        {
            messageBroker.SendMessage($"Entering state: {Name}");
        }

        public override void OnExit(OnExitData data)
        {
            messageBroker.SendMessage($"Exiting state: {Name}");
        }
    }
}