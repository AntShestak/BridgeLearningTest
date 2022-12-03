using UnityEngine;
using UnityEngine.UI;

namespace BridgeLearningTest
{

    public sealed class CanvasController : MonoBehaviour
    {

        public static CanvasController Instance { get; private set; }

        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _timeText;
        [SerializeField] private Text _objectsCollectedText;

        private ScreenFader _screenFader;
        private InfoPanel _infoPanel;
        private GameEndPanel _gameEndPanel;


        private void Awake()
        {
            #region Singleton

            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            #endregion

            _screenFader = this.GetComponentInChildren<ScreenFader>();
            _infoPanel = this.GetComponentInChildren<InfoPanel>();
            _gameEndPanel = this.GetComponentInChildren<GameEndPanel>();
        }

     
        public void FadeScreenOnStart() {

            _screenFader.FadeOut();
        }

        public void FadeScreenOnEnd()
        {

            _screenFader.FadeIn();
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = "Score: " + score;
        }

        public void SetObjectsCollectedText(int num)
        {
            _objectsCollectedText.text = "Collected Objects: " + num;
        }

        public void DisplayInfoText(string primary, string secondary = "")
        {
            _infoPanel.DisplayText(primary,secondary);
        }

        public void DisplayGameEndPanel(string primary, string secondary = "")
        {
            _gameEndPanel.DisplayPanel(primary,secondary);
        }

        public void SetTimeText(int seconds)
        {
            int min = seconds / 60;
            int sec = seconds % 60;

            if(sec < 10)
                _timeText.text = "Time: " + min + ":0" + sec;
            else
                _timeText.text = "Time: " + min + ":" + sec;
        }
    }
}


