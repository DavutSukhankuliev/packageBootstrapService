# VGBootstrapSystemDocumentation
<details><summary>Details</summary>

##### 1. Purpose
##### 2. Dependencies
##### 3. Technologies
##### 4. Installation
##### 5. How to use

</details>

# Purpose
The package was created to simplify the work with bootstrap via [Zenject](https://github.com/modesttree/Zenject).

# Dependencies

- Zenject 9.2.0-stcf3;
- SDTCore 1.0.0;
- UniRX 7.1.0;
- UniTask 2.2.5;

# Technologies

- Dependency Injection;
- Pattern: Command.

# Installation

To install package you need to click `Add project from git URL` and paste link `https://github.com/DavutSukhankuliev/packageBootstrapService.git?path=/Packages/com.sdt.bootstrapService#v1.0.0`

# How to use

First of all you need to create `*YourIsntaller* : MonoInstaller<*YourInstaller*>` and bind required containers.

```c#
public class BootInstallerDemo : MonoInstaller<BootInstallerDemo>
{
    public override void InstallBindings()
    {
        Container
            .Bind<IBootstrap>()
            .To<Bootstrap>()
            .AsSingle();

        Container
            .Bind<CommandStorage>()
            .AsSingle();
    }
}
```

Then the class which inherites the `Command.cs` need to be created in order to define the logic of your command.

```c#
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
```
    Here you will be needed a VGCore package which has ready-to-use Command pattern

Then you need to create some class which will run the Bootstrap. In this case the Monobehaviour class was used.

```c#
public class Runbootstrap : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    private BootstrapDemo _currentRun;
    private IInstantiator _instantiator;

    [Inject]
    private void Inject(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    private void Start()
    {
        StartCoroutine(RunBootstrap());
    }

    private IEnumerator RunBootstrap()
    {
        yield return new WaitForSecondsRealtime(2f);
        
        _currentRun = new BootstrapDemo();
        _currentRun.ProgressUpdate += UpdateProgressSlider;
        Debug.Log("Bootstrap created");

        for (int i = 0; i < 10; i++)
        {
            _currentRun.AddCommand(_instantiator.Instantiate<BootStartedCommand>(new object[]{i}));
        }
        
        yield return new WaitForSecondsRealtime(1f);
        
        Debug.Log("All commands added to command storage");
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        
        _currentRun.StartExecute();
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentRun.IsExecuting)
            {
                _currentRun.PauseExecution();
                Debug.Log("Paused");
            }
            else
            {
                _currentRun.ResumeExecution();
                Debug.Log("Resumed");
            }
        }
    }

    private void UpdateProgressSlider(object sender, float value)
    {
        _slider.value = value;
    }
}
```

The work of the current example is shown in the samples of the package.