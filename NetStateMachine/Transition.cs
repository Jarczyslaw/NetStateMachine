using System;

namespace NetStateMachine
{
    public class Transition : IEquatable<Transition>
    {
        public Type SourceState { get; }
        public Type TargetState { get; }

        public Transition(Type sourceState, Type targetState)
        {
            SourceState = sourceState;
            TargetState = targetState;
        }

        public bool Equals(Transition other)
        {
            if (other == null)
            {
                return false;
            }

            return SourceState == other.SourceState && TargetState == other.TargetState;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Transition);
        }

        public override int GetHashCode()
        {
            return (SourceState, TargetState).GetHashCode();
        }
    }
}