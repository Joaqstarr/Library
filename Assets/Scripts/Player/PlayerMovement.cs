using System;
using DG.Tweening;
using Level.MovingPlatform;
using Unity.VisualScripting;
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

        [SerializeField] private LayerMask _groundLayers;

        [SerializeField] private Transform _art;
        
        private bool _isGrounded
        {
            get
            {
                return _characterController.isGrounded || Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, 1.1f, _groundLayers);
            }
        }

        private CharacterController _characterController;
        private Rigidbody _rigidbody;
        private PlayerControls _playerControls;
        private Transform _cameraTransform;

        private float _jumpBufferTimer = 0;

        public bool RotateCharacterToMovement = true;

        private bool _isLaunching = false;
        private bool _launchStarted = false;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _characterController = GetComponent<CharacterController>();
            _playerControls = GetComponent<PlayerControls>();
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            CheckIfLaunchEnded();

            if (_isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = -2f; // Small negative value to keep the player grounded
            }
            
            
            HandleMovementAndRotation();


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

        private void HandleMovementAndRotation()
        {
            if(!_isLaunching){

                Vector2 moveInput = _playerControls.MoveInput;
                Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

                if (direction.magnitude >= 0.1f)
                {
                    if (RotateCharacterToMovement)
                    {
                        // Calculate the direction relative to the camera
                        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                            _cameraTransform.eulerAngles.y;
                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                            ref _turnSmoothVelocity,
                            _turnSmoothTime);
                        transform.rotation = Quaternion.Euler(0f, angle, 0f);

                        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                        _characterController.Move(moveDirection * _speed * Time.deltaTime);
                    }
                    else
                    {
                        Vector3 forwardMovement = transform.forward * _speed * moveInput.y;
                        Vector3 rightMovement = transform.right * _speed * moveInput.x;

                        Vector3 moveVector = forwardMovement + rightMovement;
                        _characterController.Move(moveVector * Time.deltaTime);
                    }


                }
            }
        }

        private void CheckIfLaunchEnded()
        {

            if (!_isLaunching) return;
            
            if(_rigidbody.velocity.magnitude != 0)
                _art.transform.forward = _rigidbody.velocity;
            
            if(!_launchStarted && _rigidbody.velocity.magnitude > 3)
            {
                _launchStarted = true;
            }

            if (_launchStarted && _rigidbody.velocity.magnitude < 3)
            {

                Vector3 right = _art.right;
                Vector3 newForward = Vector3.Cross(right, Vector3.up);

                transform.forward = newForward;
                _art.localEulerAngles = Vector3.zero;
                
                _isLaunching = false;
                _characterController.enabled = true;
                _rigidbody.isKinematic = true;
            }
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

        public bool IsGrounded()
        {
            return _isGrounded;
        }

        public void LaunchCharacter(Vector3 launchVector)
        {
            _characterController.enabled = false;
            _launchStarted = false;
            _rigidbody.isKinematic = false;
            _isLaunching = true;
            _rigidbody.AddForce(launchVector, ForceMode.Impulse);
        }

        public void Teleport(Vector3 position)
        {
            _characterController.enabled = false;
            _characterController.transform.position = position;
            _characterController.enabled = true;
        }


    }
}