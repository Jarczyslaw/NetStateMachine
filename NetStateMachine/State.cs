using NetStateMachine.Data;

namespace NetStateMachine
{
    public abstract class State
    {
        public string Name { get; set; }

        public virtual void OnEnter(OnEnterData data, object parameter)
        {
        }

        public virtual object OnExit(OnExitData data)
        {
            return null;
        }
    }
}