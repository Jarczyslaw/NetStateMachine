using NetStateMachine.Data;
using System;

namespace NetStateMachine
{
    public delegate bool OnTransitionExecuteHandler(TransitionData data);

    public class Transition
    {
        private OnTransitionExecuteHandler onTransitionExecute;

        public event OnTransitionExecuteHandler OnTransitionExecute
        {
            add => onTransitionExecute += value;
            remove => onTransitionExecute -= value;
        }

        public Type SourceStateType { get; protected set; }
        public Type TargetStateType { get; protected set; }

        public Transition(Type sourceStateType, Type targetStateType)
        {
            ValidTypes(sourceStateType);
            ValidTypes(targetStateType);

            SourceStateType = sourceStateType;
            TargetStateType = targetStateType;
        }

        private void ValidTypes(Type stateType)
        {
            if (!stateType.IsSubclassOf(typeof(State)))
            {
                throw new Exception("State type should be a subclass of State");
            }
        }

        public virtual bool Execute(TransitionData data)
        {
            if (onTransitionExecute != null)
            {
                return onTransitionExecute.Invoke(data);
            }
            return true;
        }
    }
}