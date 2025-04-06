using Level.MovingPlatform;
using UnityEngine;

namespace Player
{
    public class PlayerMovingPlatformHandler : MonoBehaviour
    {
        private CharacterController _characterController;
        private MovingPlatformVelocityTransfer _currentPlatform;
        private float _platformTimer = 0f;

        [Tooltip("How long to stay attached to the platform after contact is lost.")]
        public float graceTime = 0.1f;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_currentPlatform != null)
            {
                _characterController.Move(_currentPlatform.CurrentDelta);
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

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("MovingPlatform"))
            {
                _currentPlatform = hit.gameObject.GetComponent<MovingPlatformVelocityTransfer>();
                _platformTimer = graceTime;
            }
        }
    }
}