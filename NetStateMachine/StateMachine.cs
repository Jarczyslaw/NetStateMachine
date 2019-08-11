using NetStateMachine.Exceptions;
using NetStateMachine.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetStateMachine
{
    public class StateMachine
    {
        public Dictionary<Type, State> States { get; } = new Dictionary<Type, State>();
        public Dictionary<Transition, Func<TransitionData, bool>> Transitions { get; } = new Dictionary<Transition, Func<TransitionData, bool>>();

        public State CurrentState { get; private set; }
        public string CurrentStateName => CurrentState?.Name;

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

        public StateMachine AddTransition<TFrom, TTo>()
            where TFrom : State
            where TTo : State
        {
            return AddTransition<TFrom, TTo>(_ => true);
        }

        public StateMachine AddTransition<TFrom, TTo>(Func<TransitionData, bool> condition)
            where TFrom : State
            where TTo : State
        {
            var newTransition = new Transition(typeof(TFrom), typeof(TTo));
            if (Transitions.ContainsKey(newTransition))
            {
                throw new TransitionCurrentlyExistsException(newTransition);
            }

            Transitions.Add(newTransition, condition);
            return this;
        }

        public void Execute()
        {
            var transitions = Transitions.Keys.Where(t => t.SourceState == CurrentState.GetType());
            if (!transitions.Any())
            {
                throw new TransitionNotExistsException(CurrentState.GetType());
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
                        TargetState = GetState(transition.TargetState)
                    };

                    if (Transitions[transition].Invoke(data))
                    {
                        if (successedTransition == null)
                        {
                            successedTransition = transition;
                        }
                        else
                        {
                            throw new TooManyTransitionsException(CurrentState.GetType());
                        }
                    }
                }

                if (successedTransition != null)
                {
                    var newState = GetState(successedTransition.TargetState);
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
            var transition = Transitions.Keys.SingleOrDefault(t => t.SourceState == CurrentState.GetType() && t.TargetState == typeof(T));
            if (transition == null)
            {
                throw new TransitionNotExistsException(CurrentState.GetType(), typeof(T));
            }
            else
            {
                var targetState = GetState(transition.TargetState);
                var data = new TransitionData
                {
                    StateMachine = this,
                    SourceState = CurrentState,
                    TargetState = targetState
                };

                if (Transitions[transition].Invoke(data))
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
            return Transitions.Keys.Where(t => t.SourceState == typeof(T));
        }
    }
}