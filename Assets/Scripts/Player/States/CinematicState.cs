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
            
            Debug.Log("entering cinematic state");
        }

        protected override void OnExitState()
        {
            base.OnExitState();
            
            Debug.Log("Exiting cinematic state");
        }
    }
}