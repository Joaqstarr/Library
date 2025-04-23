using UnityEngine;

namespace Player.States
{
    public class CinematicState : BaseState
    {
        public CinematicState(PlayerStateManager playerStateManager) : base(playerStateManager)
        {
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();

            _playerMovement.enabled = false;
            _playerStateManager.PlayerAttackManagerInstance.enabled = false;
        }

        protected override void OnExitState()
        {
            base.OnExitState();
            _playerStateManager.PlayerAttackManagerInstance.enabled = true;
            
        }
    }
}