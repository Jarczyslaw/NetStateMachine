using System;

namespace NetStateMachine.Exceptions
{
    public class TooManyTransitionsException : Exception
    {
        public TooManyTransitionsException(Type sourceState)
            : base($"There are more than one valid transitions for state {sourceState.Name}")
        {
        }
    }
}