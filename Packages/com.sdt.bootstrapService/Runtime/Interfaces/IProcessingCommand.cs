using System;
using SDTCore;

namespace SDTBootstrapService
{
    public interface IProcessingCommand
    {
        event EventHandler AllCommandDone;

        bool IsExecuting { get; }

        void AddCommand(ICommand command);
        void StartExecute();
        void Clear();

        bool Any();
    }
}