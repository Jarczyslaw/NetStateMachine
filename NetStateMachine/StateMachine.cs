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
            if (!States.ContainsKey(stateType))
            {
                throw new StateNotExistsException(stateType);
            }
            return States[stateType];
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
            States.Add(stateType, state);

            if (States.Count == 0)
            {
                CurrentState = state;
            }
            return this;
        }

        private void CheckTransitionState(Type stateType)
        {
            if (!States.ContainsKey(stateType))
            {
                throw new StateNotExistsException(stateType);
            }
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

        public void Execute()
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
                    var data = new TransitionData
                    {
                        StateMachine = this,
                        SourceState = CurrentState,
                        TargetState = GetState(transition.TargetStateType)
                    };

                    if (transition.Execute(data))
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
                    SwitchStates(newState);
                }
            }
        }

        public void SkipTo<T>(bool invokeEvents = true)
            where T : State
        {
            var newState = GetState<T>();
            SwitchStates(newState, invokeEvents);
        }

        public void SwitchTo<T>()
            where T : State
        {
            var transition = Transitions.Values.SingleOrDefault(t => t.SourceStateType == CurrentStateType && t.TargetStateType == typeof(T));
            if (transition == null)
            {
                throw new TransitionNotExistsException(CurrentStateType, typeof(T));
            }
            else
            {
                var targetState = GetState(transition.TargetStateType);
                var data = new TransitionData
                {
                    StateMachine = this,
                    SourceState = CurrentState,
                    TargetState = targetState
                };

                if (transition.Execute(data))
                {
                    SwitchStates(targetState);
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

        public IEnumerable<Transition> GetStateTransitions<T>()
            where T : State
        {
            return Transitions.Values.Where(t => t.SourceStateType == typeof(T));
        }
    }
}