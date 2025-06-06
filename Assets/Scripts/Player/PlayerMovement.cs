﻿using System;
using System.Collections;
using Audio;
using Cinemachine;
using DG.Tweening;
using Level.MovingPlatform;
using Player.Animation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private float _coyoteTime = 0.5f;
        [SerializeField] private PlayerAnimationManager _animationManager;
        [FormerlySerializedAs("_jumpSoundPlayer")] [SerializeField] private RandomClipPlayer _playerJumpSound;
        [SerializeField] private RandomClipPlayer _playerFallSound;

        private float _coyoteTimer = 0;
        private float _turnSmoothVelocity;
        private float _verticalVelocity;

        [SerializeField] private LayerMask _groundLayers;

        [SerializeField] private Transform _art;

        private CinemachineStateDrivenCamera _cinemachineStateDrivenCamera;
        private bool _isGrounded
        {
            get
            {
                RaycastHit hit;
                return _characterController.isGrounded || GroundedRaycast(out hit);
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
        
        private float _postJumpTimer = 0;
        
        private void Awake()
        {

            _cinemachineStateDrivenCamera = GetComponentInChildren<CinemachineStateDrivenCamera>();
            _animationManager = GetComponent<PlayerAnimationManager>();
            _rigidbody = GetComponent<Rigidbody>();
            _characterController = GetComponent<CharacterController>();
            _playerControls = GetComponent<PlayerControls>();
        }

        private void Update()
        {
            if (_cameraTransform == null)
            {
                _cameraTransform = Camera.main.transform;

            }
            SetAnimatorGroundedCheck();
            
            CheckIfLaunchEnded();

            ApplyGroundedGravity();
            
            HandleMovementAndRotation();
            
            HandleCoyoteTime();
            HandleJumping();

            HandleGravity();
        }

        public void SetAnimatorGroundedCheck()
        {
            _animationManager.Grounded = _isGrounded;
        }

        private void ApplyGroundedGravity()
        {
            if (_isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = -2f; // Small negative value to keep the player grounded
            }
        }

        private void HandleGravity()
        {
            // Apply gravity
            _verticalVelocity += _gravity * Time.deltaTime;
            _characterController.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
        }

        private void HandleJumping()
        {
            // Handle jumping
            if (_coyoteTimer > 0 && _jumpBufferTimer > 0)
            {
                _jumpBufferTimer = 0;
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
                _playerJumpSound.PlayRanClip();
                _animationManager.SetJumpTrigger();
                _postJumpTimer = 0.2f;
            }
            
            _postJumpTimer -= Time.deltaTime;
            _jumpBufferTimer -= Time.deltaTime;
        }

        private void HandleCoyoteTime()
        {
            if(_isGrounded )
                _coyoteTimer = _coyoteTime;
            else
                _coyoteTimer -= Time.deltaTime;
            
            if(_postJumpTimer > 0)
                _coyoteTimer = 0;
        }

        private void HandleMovementAndRotation()
        {
            if(!_isLaunching){

                Vector2 moveInput = _playerControls.MoveInput;
                Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

                if (direction.magnitude >= 0.1f)
                {
                    Vector3 moveVector = Vector3.zero;
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
                        
                        moveVector = moveDirection * _speed;
                    }
                    else
                    {
                        Vector3 forwardMovement = transform.forward * _speed * moveInput.y;
                        Vector3 rightMovement = transform.right * _speed * moveInput.x;

                        moveVector = forwardMovement + rightMovement;
                    }

                    _characterController.Move(moveVector * Time.deltaTime);
                    SetAnimatorMovementSpeed(moveVector.magnitude);
                }
                else
                {
                    SetAnimatorMovementSpeed(0);
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
                
                _playerJumpSound.PlayRanClip();
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
            _animationManager.SetLaunchTrigger();
            _characterController.enabled = false;
            _launchStarted = false;
            _rigidbody.isKinematic = false;
            _isLaunching = true;
            _rigidbody.AddForce(launchVector, ForceMode.Impulse);
            _playerFallSound.PlayRanClip();
        }

        public void Teleport(Vector3 position)
        {
            _characterController.enabled = false;


            _characterController.transform.position = position;
            _characterController.enabled = true;

            _cinemachineStateDrivenCamera.enabled = true;
        }
        
        
        public void ResetVelocity()
        {
            _verticalVelocity = 0;
            _rigidbody.velocity = Vector3.zero;

            _characterController.SimpleMove(Vector3.zero);
        }

        public bool GroundedRaycast(out RaycastHit hit)
        {
            return Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 0.6f, _groundLayers);
        }
        
        public void SetAnimatorMovementSpeed(float speed)
        {
            _animationManager.MoveSpeed = speed;
        }
    }
}