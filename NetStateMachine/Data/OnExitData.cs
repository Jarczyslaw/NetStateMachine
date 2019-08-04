namespace NetStateMachine.Data
{
    public class OnExitData : EventData
    {
        public State TargetState { get; set; }
    }
}