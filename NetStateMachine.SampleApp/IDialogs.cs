using System;

namespace NetStateMachine.SampleApp
{
    public interface IDialogs
    {
        void Exception(Exception exc);

        void Information(string message);

        bool YesNoQuestion(string question);
    }
}