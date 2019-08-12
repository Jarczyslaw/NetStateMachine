using NetStateMachine.SampleApp.Views;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System.Reflection;
using System.Windows;

namespace NetStateMachine.SampleApp
{
    public partial class App : PrismApplication
    {
        private readonly MessageBroker messageBroker;
        private readonly StateMachineProvider stateMachineProvider;

        public App()
        {
            messageBroker = new MessageBroker();
            stateMachineProvider = new StateMachineProvider(messageBroker);
        }

        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IStateMachineProvider>(stateMachineProvider);
            containerRegistry.RegisterInstance(messageBroker);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewModelResolver = new ViewModelResolver();
                return viewModelResolver.Resolve(Assembly.GetExecutingAssembly(), viewType);
            });
        }
    }
}