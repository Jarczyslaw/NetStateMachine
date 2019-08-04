namespace NetStateMachine.Data
{
    public class OnExitData : EventData
    {
        public State TargetState { get; set; }
        public object Output { get; set; }
    }
}