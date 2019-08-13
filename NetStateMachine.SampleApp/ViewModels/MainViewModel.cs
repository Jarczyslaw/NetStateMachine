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
        private DelegateCommand executeCommand;
        private DelegateCommand infoCommand;
        private DelegateCommand clearCommand;
        private readonly StateMachine stateMachine;
        private CommandViewModel selectedCommand;
        private StateViewModel selectedState;

        public MainViewModel(IStateMachineProvider stateMachineProvider, MessageBroker messageBroker)
        {
            stateMachine = stateMachineProvider.GetStateMachine();
            messageBroker.OnSend += AppendStatus;

            InitializeStates();
            InitializeCommands();
            UpdateCurrentState();
        }

        public DelegateCommand InfoCommand => infoCommand ?? (infoCommand = new DelegateCommand(() =>
        {
            var sb = new StringBuilder();
            sb.AppendLine("State machine summary");
            sb.AppendLine($"States ({stateMachine.States.Count}):");
            foreach (var pair in stateMachine.States)
            {
                sb.Append('\t')
                    .AppendLine(pair.Value.Name);
            }
            sb.AppendLine($"Transistions ({stateMachine.Transitions.Count}):");
            foreach (var pair in stateMachine.Transitions)
            {
                var trans = pair.Value;
                sb.Append('\t')
                    .AppendLine($"{trans.GetType().Name} - from: {trans.SourceStateType.Name} to: {trans.TargetStateType.Name}");
            }

            Status += Environment.NewLine + sb.ToString() + Environment.NewLine;
        }));

        public DelegateCommand ClearCommand => clearCommand ?? (clearCommand = new DelegateCommand(() =>
        {
            if (MessageBoxes.YesNoQuestion("Do you really want to perform status clear?"))
            {
                Status = string.Empty;
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
                MessageBoxes.Exception(exc);
            }
        }));

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
                    Command = () => stateMachine.Execute()
                },
                new CommandViewModel
                {
                    Name = "SwitchTo",
                    Command = () => stateMachine.SwitchTo(SelectedState.StateType)
                },
                new CommandViewModel
                {
                    Name = "SkipTo",
                    Command = () => stateMachine.SkipTo(SelectedState.StateType)
                }
            };
            SelectedCommand = Commands.First();
        }

        private void AppendStatus(string message)
        {
            Status += message + Environment.NewLine;
        }
    }
}