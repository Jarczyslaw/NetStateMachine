using System;

namespace NetStateMachine.Exceptions
{
    public class TransitionCurrentlyExistsException : Exception
    {
        public TransitionCurrentlyExistsException(Transition transition)
            : this(transition.SourceStateType, transition.TargetStateType)
        {
        }

        public TransitionCurrentlyExistsException(Type sourceState, Type targetState)
            : base($"Transition from {sourceState.Name} to {targetState.Name} already exists")
        {
        }
    }
}