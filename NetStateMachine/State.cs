using NetStateMachine.Data;

namespace NetStateMachine
{
    public delegate void OnStateEnterHandler(OnEnterData data);

    public delegate void OnStateExitHandler(OnExitData data);

    public abstract class State
    {
        private OnStateEnterHandler onStateEnter;

        public event OnStateEnterHandler OnStateEnter
        {
            add => onStateEnter += value;
            remove => onStateEnter -= value;
        }

        private OnStateExitHandler onStateExit;

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

        public virtual void OnExit(OnExitData data)
        {
            onStateExit?.Invoke(data);
        }
    }
}