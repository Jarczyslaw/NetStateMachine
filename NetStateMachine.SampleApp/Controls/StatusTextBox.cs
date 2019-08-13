using System.Windows;
using System.Windows.Controls;

namespace NetStateMachine.SampleApp.Controls
{
    public class StatusTextBox : TextBox
    {
        public StatusTextBox()
        {
            AcceptsReturn = true;
            TextWrapping = TextWrapping.Wrap;
            IsReadOnly = true;
            HorizontalScrollBarVisibility
                = VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

            TextChanged += StatusTextBox_TextChanged;
        }

        private void StatusTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                CaretIndex = Text.Length;
                ScrollToEnd();
            }
        }
    }
}