using NetStateMachine.Data;

namespace NetStateMachine
{
    public abstract class State
    {
        public string Name { get; set; }

        public virtual void OnEnter(OnEnterData data)
        {
        }

        public virtual void OnExit(OnExitData data)
        {
        }
    }
}