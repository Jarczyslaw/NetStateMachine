using NetStateMachine.Data;

namespace NetStateMachine
{
    public delegate void OnStateEnterHandler(OnEnterData data);

    public delegate object OnStateExitHandler(OnExitData data);

    public delegate object OnStateTryExitHandler(OnTryExitData data);

    public abstract class State
    {
        private OnStateEnterHandler onStateEnter;
        private OnStateTryExitHandler onStateTryExit;
        private OnStateExitHandler onStateExit;

        public event OnStateEnterHandler OnStateEnter
        {
            add => onStateEnter += value;
            remove => onStateEnter -= value;
        }

        public event OnStateTryExitHandler OnStateTryExit
        {
            add => onStateTryExit += value;
            remove => onStateTryExit -= value;
        }

        public event OnStateExitHandler OnStateExit
        {
            add => onStateExit += value;
            remove => onStateExit -= value;
        }

        public string Name { get; set; }

        public virtual void OnEnter(OnEnterData data)
        {
            onStateEnter?.Invoke(data);
        }

        public virtual void OnTryExit(OnTryExitData data)
        {
            if (onStateTryExit != null)
            {
                data.TransitionArgument = onStateTryExit.Invoke(data);
            }
        }

        public virtual void OnExit(OnExitData data)
        {
            if (onStateExit != null)
            {
                data.Output = onStateExit.Invoke(data);
            }
        }
    }
}