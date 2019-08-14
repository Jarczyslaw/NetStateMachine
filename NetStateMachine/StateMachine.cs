using NetStateMachine.Data;
using NetStateMachine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetStateMachine
{
    public class StateMachine
    {
        public Dictionary<Type, State> States { get; } = new Dictionary<Type, State>();
        public Dictionary<Type, Transition> Transitions { get; } = new Dictionary<Type, Transition>();

        public State CurrentState { get; private set; }
        public string CurrentStateName => CurrentState?.Name;
        public Type CurrentStateType => CurrentState?.GetType();

        public State GetState<T>()
            where T : State
        {
            return GetState(typeof(T));
        }

        public State GetState(Type stateType)
        {
            if (States.TryGetValue(stateType, out State state))
            {
                return state;
            }
            else
            {
                throw new StateNotExistsException(stateType);
            }
        }

        public StateMachine AddState<T>()
            where T : State, new()
        {
            return AddState(new T());
        }

        public StateMachine AddState<T>(T state)
            where T : State
        {
            var stateType = typeof(T);
            if (States.ContainsKey(stateType))
            {
                throw new StateCurrentlyExistsException(stateType);
            }

            if (States.Count == 0)
            {
                CurrentState = state;
            }
            States.Add(stateType, state);
            return this;
        }

        private void CheckTransitionState(Type stateType)
        {
            if (!States.ContainsKey(stateType))
            {
                throw new StateNotExistsException(stateType);
            }
        }

        public StateMachine AddTransition<T>()
            where T : Transition, new()
        {
            return AddTransition(new T());
        }

        public StateMachine AddTransition(Transition transition)
        {
            var transitionType = transition.GetType();
            if (Transitions.ContainsKey(transitionType)
                || Transitions.Values.Any(t => t.SourceStateType == transition.SourceStateType && t.TargetStateType == transition.TargetStateType))
            {
                throw new TransitionCurrentlyExistsException(transition);
            }

            CheckTransitionState(transition.SourceStateType);
            CheckTransitionState(transition.TargetStateType);

            Transitions.Add(transitionType, transition);
            return this;
        }

        private bool PerformExecute(Transition transition, bool callOnTryExit)
        {
            var targetState = GetState(transition.TargetStateType);
            var tryExitData = new OnTryExitData
            {
                StateMachine = this,
                TargetState = targetState
            };

            if (callOnTryExit)
            {
                CurrentState.OnTryExit(tryExitData);
            }

            var data = new TransitionData
            {
                StateMachine = this,
                SourceState = CurrentState,
                TargetState = GetState(transition.TargetStateType),
                TransitionArgument = tryExitData.TransitionArgument
            };

            return transition.Execute(data);
        }

        public void Execute(bool invokeEvents = true)
        {
            var transitions = Transitions.Values.Where(t => t.SourceStateType == CurrentStateType);
            if (!transitions.Any())
            {
                throw new TransitionNotExistsException(CurrentStateType);
            }
            else
            {
                Transition successedTransition = null;
                foreach (var transition in transitions)
                {
                    var success = PerformExecute(transition, transition == transitions.First());
                    if (success)
                    {
                        if (successedTransition == null)
                        {
                            successedTransition = transition;
                        }
                        else
                        {
                            throw new TooManyTransitionsException(CurrentStateType);
                        }
                    }
                }

                if (successedTransition != null)
                {
                    var newState = GetState(successedTransition.TargetStateType);
                    SwitchStates(newState, invokeEvents);
                }
            }
        }

        public void SkipTo<T>(bool invokeEvents = true)
            where T : State
        {
            SkipTo(typeof(T), invokeEvents);
        }

        public void SkipTo(Type stateType, bool invokeEvents = true)
        {
            var newState = GetState(stateType);
            SwitchStates(newState, invokeEvents);
        }

        public void SwitchTo<T>(bool invokeEvents = true)
            where T : State
        {
            SwitchTo(typeof(T), invokeEvents);
        }

        public void SwitchTo(Type stateType, bool invokeEvents = true)
        {
            var transition = Transitions.Values.SingleOrDefault(t => t.SourceStateType == CurrentStateType && t.TargetStateType == stateType);
            if (transition == null)
            {
                throw new TransitionNotExistsException(CurrentStateType, stateType);
            }
            else
            {
                var success = PerformExecute(transition, true);
                if (success)
                {
                    var targetState = GetState(transition.TargetStateType);
                    SwitchStates(targetState, invokeEvents);
                }
            }
        }

        private void SwitchStates(State targetState, bool invokeEvents = true)
        {
            object parameter = null;
            var sourceState = CurrentState;
            if (invokeEvents)
            {
                var data = new OnExitData
                {
                    StateMachine = this,
                    TargetState = targetState
                };
                CurrentState.OnExit(data);
                parameter = data.Output;
            }

            CurrentState = targetState;

            if (invokeEvents)
            {
                var data = new OnEnterData
                {
                    StateMachine = this,
                    SourceState = sourceState,
                    Input = parameter
                };
                CurrentState.OnEnter(data);
            }
        }

        public IEnumerable<Transition> GetStateTransitions(State state)
        {
            return GetStateTransitions(state.GetType());
        }

        public IEnumerable<Transition> GetStateTransitions(Type stateType)
        {
            return Transitions.Values.Where(t => t.SourceStateType == stateType);
        }

        public IEnumerable<Transition> GetStateTransitions<T>()
            where T : State
        {
            return GetStateTransitions(typeof(T));
        }
    }
}