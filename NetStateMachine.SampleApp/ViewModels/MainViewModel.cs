using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NetStateMachine.SampleApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string status;
        private string messages;
        private DelegateCommand executeCommand;
        private DelegateCommand clearCommand;
        private readonly StateMachine stateMachine;
        private CommandViewModel selectedCommand;
        private StateViewModel selectedState;
        private readonly IDialogs dialogs;
        private bool fireEvents = true;

        public MainViewModel(IStateMachineProvider stateMachineProvider, IMessageBroker messageBroker, IDialogs dialogs)
        {
            this.dialogs = dialogs;

            stateMachine = stateMachineProvider.GetStateMachine();
            messageBroker.OnSend += AppendMessage;

            InitializeStates();
            InitializeCommands();
            UpdateCurrentState();
            SetStateMachineInfo();
        }

        public DelegateCommand ClearCommand => clearCommand ?? (clearCommand = new DelegateCommand(() =>
        {
            if (dialogs.YesNoQuestion("Do you really want to perform status clear?"))
            {
                Messages = string.Empty;
            }
        }));

        public DelegateCommand ExecuteCommand => executeCommand ?? (executeCommand = new DelegateCommand(() =>
        {
            try
            {
                SelectedCommand.Execute();
                UpdateCurrentState();
            }
            catch (Exception exc)
            {
                dialogs.Exception(exc);
            }
        }));

        public bool FireEvents
        {
            get => fireEvents;
            set => SetProperty(ref fireEvents, value);
        }

        public ObservableCollection<CommandViewModel> Commands { get; set; }

        public CommandViewModel SelectedCommand
        {
            get => selectedCommand;
            set => SetProperty(ref selectedCommand, value);
        }

        public ObservableCollection<StateViewModel> States { get; set; }

        public StateViewModel SelectedState
        {
            get => selectedState;
            set => SetProperty(ref selectedState, value);
        }

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public string Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        public string CurrentState
        {
            get => stateMachine.CurrentStateName;
        }

        private void UpdateCurrentState()
        {
            RaisePropertyChanged(nameof(CurrentState));
        }

        private void InitializeStates()
        {
            States = new ObservableCollection<StateViewModel>(stateMachine.States.Values.Select(s => new StateViewModel
            {
                Name = s.Name,
                StateType = s.GetType()
            }));
            SelectedState = States.First();
        }

        private void InitializeCommands()
        {
            Commands = new ObservableCollection<CommandViewModel>
            {
                new CommandViewModel
                {
                    Name = "Execute",
                    Command = () => stateMachine.Execute(FireEvents)
                },
                new CommandViewModel
                {
                    Name = "SwitchTo",
                    Command = () => stateMachine.SwitchTo(SelectedState.StateType, FireEvents)
                },
                new CommandViewModel
                {
                    Name = "SkipTo",
                    Command = () => stateMachine.SkipTo(SelectedState.StateType, FireEvents)
                }
            };
            SelectedCommand = Commands.First();
        }

        private void AppendMessage(string message)
        {
            Messages += message + Environment.NewLine;
        }

        private void SetStateMachineInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("State machine summary");
            sb.AppendLine($"States ({stateMachine.States.Count}):");
            foreach (var statePair in stateMachine.States)
            {
                sb.Append($"\t{statePair.Value.Name}");

                var transitions = stateMachine.GetStateTransitions(statePair.Value);
                if (transitions.Any())
                {
                    sb.AppendLine($" - transitions ({transitions.Count()}):");
                    foreach (var transition in transitions)
                    {
                        sb.Append("\t\t")
                            .AppendLine($"{transition.GetType().Name} to: {transition.TargetStateType.Name}");
                    }
                }
                else
                {
                    sb.AppendLine();
                }
            }
            sb.AppendLine($"Transitions count: {stateMachine.Transitions.Count}");
            Status += sb.ToString();
        }
    }
}