using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BridgeLearningTest
{
    public class GameEndPanel : MonoBehaviour
    {
        [SerializeField] private Text _primary;
        [SerializeField] private Text _secondary;
        [SerializeField] private Button _restartButton;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _waitInCenterTime = 1f;

        private RectTransform _tran;


        private void Awake()
        {
            _tran = GetComponent<RectTransform>();
        }

        void Start()
        {
            SetObjectsActive(false);
            _restartButton.onClick.AddListener(RestartButtonMethod);

        }

        public void DisplayPanel(string primary, string secondary = "")
        {

            _primary.text = primary;
            _secondary.text = secondary;

            StartCoroutine("DisplayCor");
        }

        private void SetObjectsActive(bool val)
        {
            _primary.gameObject.SetActive(val);
            _secondary.gameObject.SetActive(val);
            _restartButton.gameObject.SetActive(val);
        }

        private void RestartButtonMethod()
        {
            StartCoroutine("RemoveCor");
        }

        /// <summary>
        /// Coroutine DisplayCor() animates text panel to move from right to screen center
        /// </summary>
        /// <returns></returns>
        private IEnumerator DisplayCor()
        {
            Vector2 startPosition = new Vector2(2000f, 0);
            Vector2 endPosition = new Vector2(0, 0);



            float i = 0; //interpolator

            _tran.anchoredPosition = startPosition;
            SetObjectsActive(true);

            while (i <= 1)
            {
                i += _speed * Time.deltaTime;
                _tran.anchoredPosition = Vector2.Lerp(startPosition, endPosition, Mathf.Clamp(i, 0, 1));
                yield return null;
            }

        }

        private IEnumerator RemoveCor()
        {
            Vector2 startPosition = new Vector2(0, 0);
            Vector2 endPosition = new Vector2(-2000, 0);



            float i = 0; //interpolator

            _tran.anchoredPosition = startPosition;
            

            while (i <= 1)
            {
                i += _speed * Time.deltaTime;
                _tran.anchoredPosition = Vector2.Lerp(startPosition, endPosition, Mathf.Clamp(i, 0, 1));
                yield return null;
            }

            SetObjectsActive(false);

            //Restart game
            GameController.Instance.RestartGame();

        }
    }
}