using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BridgeLearningTest
{
    public enum CollectibleType { Sphere, Capsule };

    [RequireComponent(typeof(Collider))]
    public class CollectibleObject : MonoBehaviour
    {

        public CollectibleType Type
        {
            get => _type;
            //readonly
        }

        [SerializeField] private CollectibleType _type;

        private Rigidbody _rb;
        private IEnumerator _currentCoroutine;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            
        }

        private void OnEnable()
        {
            _currentCoroutine = SpawnCoroutine();
            StartCoroutine(_currentCoroutine);

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) ObjectCollected();
        }

        private void ObjectCollected()
        {
            StopCoroutine(_currentCoroutine);

            //add score

            //return to spawn pool
            CollectibleController.Instance.CollectibleCollected(this);
        }

        /// <summary>
        /// Coroutine makes a rigidbody move up from the current position and scale up
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnCoroutine()
        {

            Vector3 startingPosition = transform.position;

            Vector3 targetPosition = transform.position + Vector3.up;

            float interpolator = 0;
            float speed = 5f;

            do
            {
                interpolator += speed * Time.deltaTime;
                _rb.MovePosition( Vector3.Lerp(startingPosition, targetPosition, Mathf.Clamp01(interpolator)));
                
                yield return null;
            }
            while (interpolator <= 1);

            _currentCoroutine = IdleCoroutine();
            StartCoroutine(_currentCoroutine);

        }


        /// <summary>
        /// Coroutine moves rigidbody up and down as well as rotates it arund world axis
        /// </summary>
        /// <returns></returns>
        IEnumerator IdleCoroutine() 
        {
            Vector3 normalPosition = transform.position;
            Vector3 amplitude = Vector3.up * 0.25f;
            Vector3 rotationSpeed = Vector3.up * 100;
            float speed = 60f;
            float angle = 0;

            while (true)
            {
                angle += speed * Time.deltaTime;
                float sine = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 pos = normalPosition + amplitude * sine;
                
                _rb.MovePosition(pos);
                _rb.MoveRotation(Quaternion.Euler(rotationSpeed * Time.deltaTime) * _rb.rotation);

                yield return null;
            }
            
        }

      
    }
}
    
