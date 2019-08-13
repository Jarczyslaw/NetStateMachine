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
        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDialogs, Dialogs>();
            containerRegistry.RegisterSingleton<IMessageBroker, MessageBroker>();
            containerRegistry.RegisterSingleton<IStateMachineProvider, StateMachineProvider>();
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