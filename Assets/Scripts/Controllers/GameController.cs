using System; 
using System.Collections;
using UnityEngine;


namespace BridgeLearningTest {
    public class GameController : MonoBehaviour
    {

        public static GameController Instance { get; private set; }

        public static event Action OnLevelStart;
        public static event Action OnLevelEnd;

        [SerializeField] private float _gameDelay = 2f;

        private IEnumerator _startLevelCor;
        private IEnumerator _endLevelCoroutine;
        private SceneController _scenes;
        private GameTimer _timer;
        private SaveData _data;
        private int _currentLevel = 0;

        private void Awake()
        {

            #region Singleton
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
            #endregion

            DontDestroyOnLoad(this.gameObject); //this object persists through scene loading

            //get references
            _scenes = this.GetComponent<SceneController>();
            _timer = this.GetComponent<GameTimer>();
            _data = this.GetComponent<SaveData>();

            
        }


        public void SceneLoaded(int buildIndex)
        {
            CanvasController.Instance.FadeScreenOnStart();

            switch (buildIndex) {

                case 0:
                    _currentLevel = 1;
                    _timer.ResetTimer();
                    ScoreController.Instance.ScoreReset();
                    _startLevelCor = StartLevelCoroutine("Level 1");
                    StartCoroutine(_startLevelCor);

                    break;
                case 1:
                    _currentLevel = 2;
                    _startLevelCor = StartLevelCoroutine("Level 2");
                    StartCoroutine(_startLevelCor);
                    break;
                case 2:
                    _currentLevel = 3;
                    _startLevelCor = StartLevelCoroutine("Level 3");
                    StartCoroutine(_startLevelCor);
                    break;
                default:
                    break;
            }
        }

        public void CheckWinGameCondition(int score)
        {
            switch (_currentLevel)
            {
                case 1:
                    if (score >= 100)
                        LevelComplete();
                    break;
                case 2:
                    if (score >= 200)
                        LevelComplete();
                    break;
                case 3:
                    if (score >= 400)
                        GameWon();
                    break;
                default:
                    break;
            }
        }

        

        private void LevelComplete()
        {
            _endLevelCoroutine = EndLevelCoroutine();
            StartCoroutine(_endLevelCoroutine);
        }

        private void GameWon()
        {
            IEnumerator winGameCor = GameEndCoroutine(true);
            StartCoroutine(winGameCor);
        }

        public void GameOver()
        {
            IEnumerator lostGameCor = GameEndCoroutine(false);
            StartCoroutine(lostGameCor);
        }

        public void RestartGame()
        {
            _scenes.LoadScene(0);
        }


        private IEnumerator StartLevelCoroutine(string levelName)
        {
            ScoreController.Instance.SetRewards(_currentLevel);
            yield return new WaitForSeconds(_gameDelay / 2);
            CanvasController.Instance.DisplayInfoText(levelName);
            

            yield return new WaitForSeconds(_gameDelay / 2);
            

            _timer.StartTimer();
            OnLevelStart?.Invoke();
        }


        private IEnumerator EndLevelCoroutine()
        {
            OnLevelEnd?.Invoke();
            _timer.PauseTimer();
            CanvasController.Instance.DisplayInfoText("Level Complete");
            CanvasController.Instance.FadeScreenOnEnd();
            yield return new WaitForSeconds(_gameDelay);

            switch (_currentLevel)
            {
                case 1:
                    _scenes.LoadScene(1); //load scene by build index
                    break;
                case 2:
                    _scenes.LoadScene(2);
                    break;
                default:
                    break;
            }
        }

        private IEnumerator GameEndCoroutine(bool isGameWon)
        {
            OnLevelEnd?.Invoke();
            _timer.PauseTimer();

            //save score data
            int score = ScoreController.Instance.Score;
            int objects = ScoreController.Instance.CollectiblesCollected;
            int time = _timer.GetTime();
            _data.SaveScoreData(score,objects,time);

            if(isGameWon)
            {
                CanvasController.Instance.DisplayGameEndPanel("YOU WON", "Your scores has been saved");
                CanvasController.Instance.FadeScreenOnEnd();
            }
            else
                CanvasController.Instance.DisplayGameEndPanel("GAME OVER", "You are blocked");
            
            
            yield return null;
        }

        
    }
}

