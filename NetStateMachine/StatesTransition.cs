namespace NetStateMachine
{
    public abstract class StatesTransition<TFrom, TTo> : Transition
    {
        protected StatesTransition()
        {
            SourceStateType = typeof(TFrom);
            TargetStateType = typeof(TTo);
        }
    }
}