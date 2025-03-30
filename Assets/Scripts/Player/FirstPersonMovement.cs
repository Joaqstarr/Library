using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class FirstPersonMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;

        private PlayerControls _playerControls;
        private CharacterController _characterController;

        [SerializeField]
        private float _gravity = -9.81f;

        private float _curGravity = 0;
        // Start is called before the first frame update
        void Start()
        {
            _playerControls = GetComponent<PlayerControls>();
            _characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {

            if (_playerControls && _characterController)
            {
                ApplyMovement(_playerControls.MoveInput);
            }
        }

        void ApplyMovement(Vector2 input)
        {

            Vector3 forwardMove = transform.forward * input.y;
            Vector3 rightMove = transform.right * input.x;
            
            Vector3 movementDir = (forwardMove + rightMove).normalized;
            
            _curGravity += _gravity * Time.deltaTime;

            if (_characterController.isGrounded)
            {
                _curGravity = Mathf.Clamp(_curGravity, _gravity, 0);
            }
            
            movementDir.y = _curGravity;
            
            _characterController.Move(movementDir * (Time.deltaTime * _moveSpeed));
        }

        bool IsGrounded()
        {
            return true;
        }
        
    }
}
