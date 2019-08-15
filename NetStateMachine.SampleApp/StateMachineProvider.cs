using NetStateMachine.SampleApp.States;
using NetStateMachine.SampleApp.Transistions;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace NetStateMachine.SampleApp
{
    public class StateMachineProvider : IStateMachineProvider
    {
        private readonly IUnityContainer container;

        public StateMachineProvider(IUnityContainer container)
        {
            this.container = container;
        }

        public StateMachine GetStateMachine()
        {
            var stateMachine = new StateMachine();

            foreach (var stateType in GetAllStates())
            {
                stateMachine.AddState(stateType, (State)container.Resolve(stateType));
            }

            foreach (var transitionType in GetAllTransitions())
            {
                stateMachine.AddTransition(transitionType, (Transition)container.Resolve(transitionType));
            }

            return stateMachine;
        }

        private List<Type> GetAllStates()
        {
            return Assembly.GetExecutingAssembly()
               .GetTypes()
               .Where(t => t.IsSubclassOf(typeof(BaseState)))
               .OrderBy(t => t.Name)
               .ToList();
        }

        private List<Type> GetAllTransitions()
        {
            var tType = typeof(IAppTransition);
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => tType.IsAssignableFrom(t) && !t.IsInterface)
                .ToList();
        }
    }
}