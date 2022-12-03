using System.Collections;
using UnityEngine;

namespace BridgeLearningTest
{
    /// <summary>
    /// Simplified timer to count seconds passed
    /// </summary>
    public class GameTimer : MonoBehaviour
    {
        private int _secondsPassed;

        private IEnumerator _timerCoroutine;
        private WaitForSeconds _waitTime; 

        private void Start()
        {
            _waitTime = new WaitForSeconds(1f);
            _timerCoroutine = TimerCoroutine();
        }

        private void OnEnable()
        {
            SceneController.OnSceneLoaded += UpdateDisplay;
        }

        private void OnDisable()
        {
            SceneController.OnSceneLoaded -= UpdateDisplay;
        }

        public void PauseTimer()
        {
            StopCoroutine(_timerCoroutine);
        }

        public void StartTimer()
        {
            StartCoroutine(_timerCoroutine);
        }

        public void ResetTimer()
        {
            _secondsPassed = 0;
        }

        public int GetTime()
        {
            return _secondsPassed;
        }

        private void UpdateDisplay()
        {
            CanvasController.Instance.SetTimeText(_secondsPassed);
        }

        private IEnumerator TimerCoroutine()
        {
            while (true)
            {
                yield return _waitTime;

                _secondsPassed++;
                UpdateDisplay();

            }
        }
    }
}