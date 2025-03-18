namespace Player.States
{
    public class LocomotionState : BaseState
    {
        public LocomotionState(PlayerStateManager playerStateManager) : base(playerStateManager)
        {
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();

            _playerMovement.enabled = true;
        }


        protected override void OnExitState()
        {
            base.OnExitState();
            
            _playerMovement.enabled = false;
        }
    }
}