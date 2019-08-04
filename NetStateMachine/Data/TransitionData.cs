namespace NetStateMachine.Data
{
    public class TransitionData : EventData
    {
        public State SourceState { get; set; }
        public State TargetState { get; set; }
    }
}