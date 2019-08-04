using System;

namespace NetStateMachine.Exceptions
{
    public class StateCurrentlyExistsException : Exception
    {
        public StateCurrentlyExistsException(Type stateType)
            : base($"State {stateType.Name} currently exists in state machine")
        {
        }
    }
}