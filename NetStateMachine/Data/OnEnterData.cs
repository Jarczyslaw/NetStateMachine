namespace NetStateMachine.Data
{
    public class OnEnterData : EventData
    {
        public State SourceState { get; set; }
        public object Input { get; set; }
    }
}