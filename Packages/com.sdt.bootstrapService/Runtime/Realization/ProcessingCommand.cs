using System;
using System.Collections.Generic;
using SDTCore;

namespace SDTBootstrapService
{
    public class ProcessingCommand : IProcessingCommand
    {
        public event EventHandler AllCommandDone;
        public bool IsExecuting { get; protected set; }
        public Queue<ICommand> Queue => _queue;

        private readonly Queue<ICommand> _queue = new Queue<ICommand>();

        protected int Count => _queue.Count;
        
        public void AddCommand(ICommand command)
        {
            if (command == null)
            {
                return;
            }
            
            _queue.Enqueue(command);
        }

        public void Clear()
        {
            _queue.Clear();
        }

        public bool Any()
        {
            return _queue.Count > 0;
        }

        public virtual void StartExecute()
        {
            
        }
        
        protected virtual void Execute()
        {
            
        }

        protected ICommand Dequeue()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }

        protected void OnComplete()
        {
            AllCommandDone?.Invoke(this,EventArgs.Empty);
        }
    }
}