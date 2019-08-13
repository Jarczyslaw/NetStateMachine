using NetStateMachine.Data;

namespace NetStateMachine.SampleApp.Transistions
{
    public class BaseTransition<TFrom, TTo> : StatesTransition<TFrom, TTo>
    {
        private readonly IMessageBroker messageBroker;

        public BaseTransition(IMessageBroker messageBroker)
        {
            this.messageBroker = messageBroker;
        }

        public override bool Execute(TransitionData data)
        {
            messageBroker.SendMessage($"Executed {GetType().Name}");
            return OnTransition(data);
        }

        public virtual bool OnTransition(TransitionData data)
        {
            return true;
        }
    }
}