using System;
using Level.MovingPlatform;
using UnityEngine;

namespace Enemies.Robot
{
    public class RobotMovingPlatformHandler : MonoBehaviour
    {
        private Rigidbody _enemyRb;
        private MovingPlatformVelocityTransfer _currentPlatform;
        private float _platformTimer = 0f;

        [Tooltip("How long to stay attached to the platform after contact is lost.")]
        public float graceTime = 0.1f;

        private void Awake()
        {
            _enemyRb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_currentPlatform != null)
            {
                _enemyRb.MovePosition(_currentPlatform.CurrentDelta);
            }

            
            // Countdown grace timer
            if (_platformTimer > 0f)
            {
                _platformTimer -= Time.deltaTime;
                if (_platformTimer <= 0f)
                {
                    _currentPlatform = null;
                }
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                _currentPlatform = other.gameObject.GetComponentInParent<MovingPlatformVelocityTransfer>();
                _platformTimer = graceTime;
            }
        }


    }
}