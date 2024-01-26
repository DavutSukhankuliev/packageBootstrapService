using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SDTBootstrapService
{
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
}