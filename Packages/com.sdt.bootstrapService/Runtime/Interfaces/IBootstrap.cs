using System;
using SDTCore;

namespace SDTBootstrapService
{
    public interface IBootstrap
    {
        event EventHandler<float> ProgressUpdate;

        void StartExecute();
        void PauseExecution();
        void ResumeExecution();
        void AddCommand(ICommand command);
    }
}