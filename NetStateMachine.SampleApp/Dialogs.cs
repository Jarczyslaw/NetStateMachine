using System;
using System.Windows;

namespace NetStateMachine.SampleApp
{
    public class Dialogs : IDialogs
    {
        public void Exception(Exception exc)
        {
            MessageBox.Show(exc.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void Information(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool YesNoQuestion(string question)
        {
            return MessageBox.Show(question, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}