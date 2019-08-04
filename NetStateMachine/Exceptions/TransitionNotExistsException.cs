using System;

namespace NetStateMachine.Exceptions
{
    public class TransitionNotExistsException : Exception
    {
        public TransitionNotExistsException(Type sourceState)
            : base($"There are no transitions for {sourceState.Name}")
        {
        }

        public TransitionNotExistsException(Type sourceState, Type targetState)
            : base($"Transition from {sourceState.Name} to {targetState.Name} does not exists")
        {
        }
    }
}