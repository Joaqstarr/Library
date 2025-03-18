namespace Player.States
{
    public class PipeState : BaseState
    {
        public PipeState(PlayerStateManager playerStateManager) : base(playerStateManager)
        {
        }


        protected override void OnEnterState()
        {
            base.OnEnterState();
            _playerMovement.enabled = false;
        }

        protected override void OnExitState()
        {
            base.OnExitState();
        }
        
        
    }
}