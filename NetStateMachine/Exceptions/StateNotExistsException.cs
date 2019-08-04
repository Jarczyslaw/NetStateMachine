using System;

namespace NetStateMachine.Exceptions
{
    public class StateNotExistsException : Exception
    {
        public StateNotExistsException(Type stateType)
            : base($"State {stateType.Name} does not exists")
        {
        }
    }
}