using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace NetStateMachine.SampleApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string status;
        private DelegateCommand executeCommand;
        private readonly StateMachine stateMachine;

        public MainViewModel(IStateMachineProvider stateMachineProvider)
        {
            stateMachine = stateMachineProvider.GetStateMachine();
        }

        public DelegateCommand ExecuteCommand => executeCommand ?? (executeCommand = new DelegateCommand(() =>
        {
        }));

        public ObservableCollection<CommandViewModel> Commands { get; set; }

        public ObservableCollection<StateViewModel> States { get; set; }

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
    }
}