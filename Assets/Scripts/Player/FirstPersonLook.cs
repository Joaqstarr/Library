using System;
using UnityEngine;

namespace Player
{
    public class FirstPersonLook : MonoBehaviour
    {
        private bool _isLookEnabled = true;
        private PlayerControls _playerControls;

        [SerializeField] private Vector2 _lookSensitivity = Vector2.one;
        [SerializeField] private Transform _verticalRotationTransform;
        [SerializeField] private Vector2 _minMaxVerticalLook = new Vector2(-90f, 90f);
        private void Start()
        {
            _playerControls = GetComponent<PlayerControls>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (_isLookEnabled && _playerControls)
            {
                AddHorizontalInput(_playerControls.LookInput.x);            
                AddVerticalInput(_playerControls.LookInput.y);            
            }
        }

        void AddHorizontalInput(float input)
        {
            transform.RotateAround(transform.position, transform.up, input * _lookSensitivity.x);
        }

        void AddVerticalInput(float input)
        {
            float newRotation = _verticalRotationTransform.localEulerAngles.x - input * _lookSensitivity.y;

            // Convert from 0-360 to -180 to 180 range for proper clamping
            if (newRotation > 180f) newRotation -= 360f;

            // Clamp between -90 and 90 degrees
            newRotation = Mathf.Clamp(newRotation, _minMaxVerticalLook.x, _minMaxVerticalLook.y);

            // Apply the clamped rotation
            _verticalRotationTransform.localRotation = Quaternion.Euler(newRotation, 0f, 0f);
        }

    }
}