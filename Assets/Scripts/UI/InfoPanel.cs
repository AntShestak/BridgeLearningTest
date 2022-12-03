using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BridgeLearningTest
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Text _primary;
        [SerializeField] private Text _secondary;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _waitInCenterTime = 1f;

        private RectTransform _tran;


        private void Awake()
        {
            _tran = GetComponent<RectTransform>();
        }

        void Start()
        {
            SetTextActive(false);


        }


        public void DisplayText(string primary, string secondary = "")
        {

            _primary.text = primary;
            _secondary.text = secondary;

            StartCoroutine("DisplayCor");
        }

        private void SetTextActive(bool val)
        {
            _primary.gameObject.SetActive(val);
            _secondary.gameObject.SetActive(val);
        }

        /// <summary>
        /// Coroutine DisplayCor() animates text panel to move from right to left of the screen stopping in the middle
        /// </summary>
        /// <returns></returns>
        private IEnumerator DisplayCor()
        {
            Vector2 startPosition = new Vector2(2000f, 0);
            Vector2 endPosition = new Vector2(-2000f, 0);



            float i = 0; //interpolator

            _tran.anchoredPosition = startPosition;
            SetTextActive(true);

            while (i <= 0.5f)
            {
                i += _speed * Time.deltaTime;
                _tran.anchoredPosition = Vector2.Lerp(startPosition, endPosition, Mathf.Clamp(i, 0, 0.5f));
                yield return null;
            }

            //wait in center
            _tran.anchoredPosition = new Vector2(0, 0);
            yield return new WaitForSeconds(_waitInCenterTime);

            //continue
            while (i <= 1.0f)
            {
                i += _speed * Time.deltaTime;
                _tran.anchoredPosition = Vector2.Lerp(startPosition, endPosition, Mathf.Clamp(i, 0.5f, 1f));
                yield return null;
            }

            SetTextActive(false);

        }
    }
}