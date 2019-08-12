using Prism.Mvvm;
using System;

namespace NetStateMachine.SampleApp.ViewModels
{
    public class StateViewModel : BindableBase
    {
        public string Name { get; set; }
        public Type StateType { get; set; } 
    }
}