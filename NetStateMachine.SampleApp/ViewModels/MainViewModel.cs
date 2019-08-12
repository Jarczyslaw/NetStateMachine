using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace NetStateMachine.SampleApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string status;
        private DelegateCommand executeCommand;
        private readonly StateMachine stateMachine;
        private CommandViewModel selectedCommand;
        private StateViewModel selectedState;

        public MainViewModel(IStateMachineProvider stateMachineProvider)
        {
            stateMachine = stateMachineProvider.GetStateMachine();
            InitializeStates();
            InitializeCommands();
            UpdateCurrentState();
        }

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
            Status += Environment.NewLine + message;
        }
    }
}