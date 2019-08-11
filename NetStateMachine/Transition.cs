using NetStateMachine.Data;
using System;

namespace NetStateMachine
{
    public delegate bool OnTransitionExecuteHandler(TransitionData data);

    public abstract class Transition
    {
        private OnTransitionExecuteHandler onTransitionExecute;

        public event OnTransitionExecuteHandler OnTransitionExecute
        {
            add => onTransitionExecute += value;
            remove => onTransitionExecute -= value;
        }

        public Type SourceStateType { get; protected set; }
        public Type TargetStateType { get; protected set; }

        public virtual bool Execute(TransitionData data)
        {
            if (onTransitionExecute != null)
            {
                return onTransitionExecute.Invoke(data);
            }
            return  true;
        }
    }
}