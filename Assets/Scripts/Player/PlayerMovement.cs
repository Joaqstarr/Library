using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        private float _turnSmoothVelocity;

        private CharacterController _characterController;
        private PlayerControls _playerControls;
        private Transform _cameraTransform;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerControls = GetComponent<PlayerControls>();
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            Vector2 moveInput = _playerControls.MoveInput;
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // Calculate the direction relative to the camera
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _characterController.Move(moveDirection * _speed * Time.deltaTime);
            }
        }
    }
}