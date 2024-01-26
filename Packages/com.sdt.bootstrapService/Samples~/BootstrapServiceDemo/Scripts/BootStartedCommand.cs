using System;
using Cysharp.Threading.Tasks;
using SDTCore;
using UnityEngine;

namespace SDTBootstrapService
{
    public class BootStartedCommand : Command
    {
        private int _debugTimer;
        
        public BootStartedCommand(CommandStorage commandStorage, int debugTimer) : base(commandStorage)
        {
            _debugTimer = debugTimer;
            Debug.Log("Boot started command created");
        }

        public override CommandResult Execute()
        {
            Test();
            return base.Execute();
        }

        private async void Test()
        {
            await UniTask.Delay(1000);
            Debug.Log($"{10-_debugTimer}");
            Done?.Invoke(this, EventArgs.Empty);
        }
    }
}