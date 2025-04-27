using UnityEngine;

namespace Player.States
{
    public class CinematicState : BaseState
    {
        public CinematicState(PlayerStateManager playerStateManager) : base(playerStateManager)
        {
        }

        private Vector3 _lastPos;
        protected override void OnEnterState()
        {
            base.OnEnterState();
            _lastPos = _playerStateManager.transform.position;
            _playerMovement.enabled = false;
            _playerStateManager.PlayerAttackManagerInstance.enabled = false;
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();
            //check if player is in a cinematic

            float deltaMovement = Vector3.Distance(_lastPos, _playerStateManager.transform.position);
            float vel = deltaMovement / Time.deltaTime;
            
            _playerStateManager.PlayerMovementInstance.SetAnimatorMovementSpeed(vel);

            _playerStateManager.PlayerMovementInstance.SetAnimatorGroundedCheck();
            
            _lastPos = _playerStateManager.transform.position;
        }
        protected override void OnExitState()
        {
            base.OnExitState();
            _playerStateManager.PlayerAttackManagerInstance.enabled = true;
            
        }
    }
}