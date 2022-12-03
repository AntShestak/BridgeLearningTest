using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BridgeLearningTest {
    public class ObstacleObject : MonoBehaviour
    {
        private IEnumerator _currentCoroutine;

        private void OnEnable()
        {
            _currentCoroutine = SpawnCoroutine();
            StartCoroutine(_currentCoroutine);
        }

        IEnumerator SpawnCoroutine()
        {
            
            Vector3 startingPosition = transform.position;

            Vector3 targetPosition = transform.position + Vector3.up * transform.lossyScale.y / 2;
           

            float interpolator = 0;
            float speed = 10f;

            do
            {
                interpolator += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startingPosition, targetPosition, Mathf.Clamp01(interpolator));
                yield return null;
            }
            while (interpolator <= 1);

            
        }
    }
}

