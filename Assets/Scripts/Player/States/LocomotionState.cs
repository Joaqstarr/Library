using Player.States.LocomotionStates;

namespace Player.States
{
    public class LocomotionState : BaseState
    {
        
        #region States
        
        private AimState _aimState;
        
        #endregion
        
        public LocomotionState(PlayerStateManager playerStateManager) : base(playerStateManager)
        {
            _aimState = new AimState(playerStateManager, this);
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();

            _playerMovement.enabled = true;
            _playerStateManager.PlayerInteractionManagerInstance.enabled = true;
        }


        protected override void OnExitState()
        {
            base.OnExitState();
            
            _playerMovement.enabled = false;
            _playerStateManager.PlayerInteractionManagerInstance.enabled = false;

        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            if (_currentState == null)
            {
                if (_playerControls.SuckPressed)
                {
                    SwitchToAimState();
                }
            }
        }

        public void SwitchToAimState()
        {
            SwitchState(_aimState);
        }
        
        public void SwitchToNullState()
        {
            SwitchState(null);
        }
    }
}