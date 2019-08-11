using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStateMachine.Exceptions;
using NetStateMachine.Tests.States;
using NetStateMachine.Tests.Transitions;

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
            stateMachine.AddState<StateA>()
                .AddState<StateB>()
                .AddState<StateC>()
                .AddState<StateD>();

            stateMachine.AddTransition<AtoB>()
                .AddTransition<BtoC>()
                .AddTransition<CtoD>();
        }

        [TestMethod]
        public void InitializeTestStates()
        {
            Assert.AreEqual(4, stateMachine.States.Count);
            Assert.AreEqual(3, stateMachine.Transitions.Count);
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
            stateMachine.AddTransition(new AtoB());
        }

        [TestMethod]
        [ExpectedException(typeof(TransitionNotExistsException))]
        public void MissingTransition()
        {
            stateMachine.SwitchTo<StateX>();
        }

        [TestMethod]
        public void SeveralSteps()
        {
            stateMachine.SwitchTo<StateB>();
            stateMachine.SwitchTo<StateC>();
            stateMachine.SwitchTo<StateD>();
            Assert.AreEqual(typeof(StateD), stateMachine.CurrentStateType);
        }

        [TestMethod]
        [ExpectedException(typeof(TooManyTransitionsException))]
        public void TooManyValidTransistionsForState()
        {
            stateMachine.AddTransition<AtoC>();
            stateMachine.Execute();
            Assert.AreEqual(typeof(StateD), stateMachine.CurrentStateType);
        }

        [TestMethod]
        public void SkipToState()
        {
            stateMachine.SkipTo<StateD>();
            Assert.AreEqual(typeof(StateD), stateMachine.CurrentStateType);
        }
    }
}