using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        [SerializeField] private float _gravity = -9.8f;
        [SerializeField] private float _jumpHeight = 1.5f;
        [SerializeField] private float _jumpInputBuffer = 0.2f;
        private float _turnSmoothVelocity;
        private float _verticalVelocity;
        private bool _isGrounded;

        private CharacterController _characterController;
        private PlayerControls _playerControls;
        private Transform _cameraTransform;

        private float _jumpBufferTimer = 0;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerControls = GetComponent<PlayerControls>();
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            _isGrounded = _characterController.isGrounded;

            if (_isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = -2f; // Small negative value to keep the player grounded
            }

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
            

            // Handle jumping
            if (_isGrounded && _jumpBufferTimer > 0)
            {
                _jumpBufferTimer = 0;
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

            // Apply gravity
            _verticalVelocity += _gravity * Time.deltaTime;
            _characterController.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
            
            _jumpBufferTimer -= Time.deltaTime;
        }


        private void OnEnable()
        {
            _playerControls.OnJumpPressed += OnJumpPressed;
        }
        private void OnDisable()
        {
            _playerControls.OnJumpPressed -= OnJumpPressed;
        }
        private void OnJumpPressed()
        {
            _jumpBufferTimer = _jumpInputBuffer;
        }
    }
}