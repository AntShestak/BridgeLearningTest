using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BridgeLearningTest {

    public sealed  class ScoreController : MonoBehaviour
    {
        public static ScoreController Instance { get; private set; }
       
        public int Score { get; private set; }
        public int CollectiblesCollected { get; private set; }
        

        private int _sphereReward;
        private int _capsuleReward;
        private Dictionary<CollectibleType,int> _rewardsByType = new Dictionary<CollectibleType, int>();
        private CollectibleType? _lastPushedType = null;

        private void Awake()
        {
            #region Singleton
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
            #endregion

        }

        private void Start()
        {
            Score = 0;
            CollectiblesCollected = 0;
        }

        private void OnEnable()
        {
            GameController.OnLevelStart += ResetLastTypeCollected;
            SceneController.OnSceneLoaded += UpdateScoreDisplay;
        }

        private void OnDisable()
        {
            GameController.OnLevelStart -= ResetLastTypeCollected;
            SceneController.OnSceneLoaded -= UpdateScoreDisplay;
        }

        public void ScoreReset()
        {
            Score = 0;
            CollectiblesCollected = 0;
            UpdateScoreDisplay();
        }

        private void ResetLastTypeCollected()
        {
            _lastPushedType = null;
        }

        public void SetRewards(int level)
        {
            

            switch (level)
            {
                case 1:
                    _sphereReward = 1;
                    _capsuleReward = 2;
                    break;
                case 2:
                    _sphereReward = 10;
                    _capsuleReward = 12;
                    break;
                case 3:
                    _sphereReward = 20;
                    _capsuleReward = 22;
                    break;
                default:
                    break;
            }

            _rewardsByType[CollectibleType.Sphere] = _sphereReward;
            _rewardsByType[CollectibleType.Capsule] = _capsuleReward;
        }

        public void AddScore(CollectibleType type)
        {
            int scoreToAdd;
            if (type == _lastPushedType)
                scoreToAdd = _rewardsByType[type] * -2; //decrease the score on double value
            else
                scoreToAdd = _rewardsByType[type];

            _lastPushedType = type;
            Score += scoreToAdd;

            CollectiblesCollected++;
            UpdateScoreDisplay();
            

            GameController.Instance.CheckWinGameCondition(Score);
        }

        private void UpdateScoreDisplay()
        {
            CanvasController.Instance.SetScoreText(Score);
            CanvasController.Instance.SetObjectsCollectedText(CollectiblesCollected);
        }


    }
}

