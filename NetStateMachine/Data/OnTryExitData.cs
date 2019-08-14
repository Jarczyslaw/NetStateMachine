namespace NetStateMachine.Data
{
    public class OnTryExitData : EventData
    {
        public State TargetState { get; set; }
        public object TransitionArgument { get; set; }
    }
}