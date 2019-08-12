using Prism.Mvvm;
using System;

namespace NetStateMachine.SampleApp.ViewModels
{
    public class CommandViewModel : BindableBase
    {
        public string Name { get; set; }
        public Action Command { get; set; }
    }
}