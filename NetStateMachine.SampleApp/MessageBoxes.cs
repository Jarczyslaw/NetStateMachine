using System;
using System.Windows;

namespace NetStateMachine.SampleApp
{
    public static class MessageBoxes
    {
        public static void Exception(Exception exc)
        {
            MessageBox.Show(exc.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Information(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}