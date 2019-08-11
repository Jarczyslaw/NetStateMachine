namespace NetStateMachine
{
    public abstract class StatesTransition<TFrom, TTo> : Transition
    {
        protected StatesTransition()
            : base(typeof(TFrom), typeof(TTo))
        {
        }
    }
}