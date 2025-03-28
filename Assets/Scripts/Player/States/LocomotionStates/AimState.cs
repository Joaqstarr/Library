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

            _playerStateManager.AimCamera.Priority = 15;
            _playerStateManager.PlayerInteractionManagerInstance.enabled = false;
        }

        protected override void OnExitState()
        {
            base.OnExitState();

            _playerMovement.enabled = true;
            _playerStateManager.AimCamera.Priority = -1;
            _playerStateManager.PlayerInteractionManagerInstance.enabled = true;
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();
            
            if (!_playerControls.SuckPressed && !_playerControls.BlowPressed)
            {
                DisableAimState();
            }


        }

        private void DisableAimState()
        {
            _locomotionState.SwitchToNullState();
        }
    }
}