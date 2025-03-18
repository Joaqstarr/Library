using Utility.StateMachine;

namespace Player.States
{
    public abstract class BaseState : HierarchalStateMachine
    {
        protected PlayerStateManager _playerStateManager;
        protected PlayerControls _playerControls;
        protected PlayerMovement _playerMovement;

        public BaseState(PlayerStateManager playerStateManager)
        {
            _playerStateManager = playerStateManager;
            _playerControls = playerStateManager.PlayerControlsInstance;
            _playerMovement = playerStateManager.PlayerMovementInstance;
        }


        protected override void OnExitState()
        {
            base.OnExitState();
            
            _playerMovement.enabled = false;

        }
    }
}