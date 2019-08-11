using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStateMachine.Exceptions;
using NetStateMachine.Tests.States;

namespace NetStateMachine.Tests
{
    [TestClass]
    public class StateMachineTests
    {
        private StateMachine stateMachine;

        [TestInitialize]
        public void Setup()
        {
            stateMachine = new StateMachine();
            stateMachine.AddState(new StateA())
                .AddState(new StateB())
                .AddState(new StateC())
                .AddState(new StateD());

            stateMachine.AddTransition<StateA, StateB>();
        }

        [TestMethod]
        public void InitializeTestStates()
        {
            Assert.AreEqual(4, stateMachine.States.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(StateCurrentlyExistsException))]
        public void AddExistingState()
        {
            stateMachine.AddState(new StateA());
        }

        [TestMethod]
        [ExpectedException(typeof(StateNotExistsException))]
        public void GetInvalidState()
        {
            stateMachine.GetState<StateX>();
        }

        [TestMethod]
        [ExpectedException(typeof(TransitionCurrentlyExistsException))]
        public void AddExistingTransistion()
        {
            stateMachine.AddTransition<StateA, StateB>();
        }
    }
}