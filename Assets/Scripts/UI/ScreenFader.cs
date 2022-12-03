using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace BridgeLearningTest {
    public class ScreenFader : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.01f;

        private Image _img;
        private IEnumerator _currentCoroutine = null;


        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        public void FadeIn() 
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = FadeInCor();
            StartCoroutine(_currentCoroutine);
        }

        public void FadeOut()
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = FadeOutCor();
            StartCoroutine(_currentCoroutine);
        }

        private IEnumerator FadeInCor()
        {
            Color current = _img.color;
            Color targetColor = new Color(current.r, current.g, current.b, 1.0f);

            do
            {
                current.a += _speed * Time.deltaTime;
                _img.color = current;
                yield return null;
            }
            while (current.a < targetColor.a);


            _img.color = targetColor;
            
        }

        private IEnumerator FadeOutCor()
        {
            Color current = _img.color;
            Color targetColor = new Color(current.r, current.g, current.b, 0.0f);

            do
            {
                current.a -= _speed * Time.deltaTime;
                _img.color = current;
                yield return null;
            }
            while (current.a > targetColor.a);


            _img.color = targetColor;

        }
    }
}

