using DG.Tweening;
using UnityEngine;

namespace Player.States.LocomotionStates
{
    public class AimState : BaseState
    {
        private LocomotionState _locomotionState;
        public AimState(PlayerStateManager playerStateManager, LocomotionState locomotionState) : base(playerStateManager)
        {
            _locomotionState = locomotionState;
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();

            Vector3 forward = -Vector3.Cross(_playerStateManager.transform.up, Camera.main.transform.right);

            _playerStateManager.transform.forward = forward;
            _playerStateManager.AimTransform.forward = Camera.main.transform.forward;
            _playerStateManager.PlayerCameraStateManagerInstance.IsAiming = true;

            _playerStateManager.PlayerInteractionManagerInstance.enabled = false;
            _playerMovement.enabled = true;
            _playerMovement.RotateCharacterToMovement = false;

        }

        protected override void OnExitState()
        {
            base.OnExitState();

            _playerMovement.enabled = true;
            _playerMovement.RotateCharacterToMovement = true;
            _playerStateManager.PlayerCameraStateManagerInstance.IsAiming = false;
            _playerStateManager.PlayerInteractionManagerInstance.enabled = true;
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            
            if ((!_playerControls.AimPressed && !_playerControls.AttackPressed) || !_playerMovement.IsGrounded())
            {
                DisableAimState();
            }

            HandleRotation();

        }

        private void HandleRotation()
        {
            Vector2 lookInput = _playerControls.LookInput;

            // Rotate character around the y-axis
            float targetAngleY = _playerStateManager.transform.eulerAngles.y + lookInput.x;
            _playerStateManager.transform.rotation = Quaternion.Euler(0f, targetAngleY, 0f);

            // Rotate aim transform around the x-axis
            if (_playerStateManager.AimTransform != null)
            {
                float targetAngleX = _playerStateManager.AimTransform.eulerAngles.x - lookInput.y;
                targetAngleX = (targetAngleX > 180) ? targetAngleX - 360 : targetAngleX; // Convert to -180 to 180 range
                targetAngleX = Mathf.Clamp(targetAngleX, -45f, 45f); // Adjust the min and max values as needed
                targetAngleX = (targetAngleX < 0) ? targetAngleX + 360 : targetAngleX; // Convert back to 0 to 360 range
                _playerStateManager.AimTransform.rotation = Quaternion.Euler(targetAngleX, _playerStateManager.AimTransform.eulerAngles.y, 0f);
            }
        }

        private void DisableAimState()
        {
            _locomotionState.SwitchToNullState();
        }
    }
}