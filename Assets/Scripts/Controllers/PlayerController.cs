using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeLearningTest {


    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {

        public bool IsInputAllowed { get; set; }

        private Rigidbody _rb;
        private float _speed = 180f;
        private Vector3 _inputs;

        private void Start()
        {
            _rb = this.GetComponent<Rigidbody>();
            _inputs = Vector3.zero;
            IsInputAllowed = false;
        }

        private void OnEnable()
        {
            GameController.OnLevelStart += EnableInputs;
            GameController.OnLevelEnd += DisableInputs;
        }

        private void OnDisable()
        {
            GameController.OnLevelStart -= EnableInputs;
            GameController.OnLevelEnd -= DisableInputs;
        }

        private void EnableInputs() => IsInputAllowed = true;

        private void DisableInputs() => IsInputAllowed = false;


        // Update is called once per frame
        void Update()
        {
            _inputs.x = Input.GetAxis("Horizontal"); 
            _inputs.z = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            if (IsInputAllowed)
                _rb.velocity = _inputs * _speed * Time.fixedDeltaTime;
            else
                _rb.velocity = Vector3.zero;
        }
    }
}

