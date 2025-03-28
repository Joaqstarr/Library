using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Robot
{
    public class RobotIdleState : RobotBaseState
    {
        private Vector3 _originalPosition;
        private Vector3 _targetPosition;


        public RobotIdleState(RobotStateManager robotStateManager) : base(robotStateManager)
        {
            _originalPosition = _robotStateManager.transform.position;
        }

        protected override void OnEnterState()
        {
            SetNewTargetPosition();
        }

        public override void OnUpdateState()
        {
            CheckIfReachedDestination();
            SearchForPlayer();
        }

        protected override void OnExitState()
        {
        }

        private void SetNewTargetPosition()
        {
            Vector2 randomDirection = Random.insideUnitCircle * Data.WanderRadius;
            _targetPosition = _originalPosition + new Vector3(randomDirection.x, 0, randomDirection.y);
            Agent.SetDestination(_targetPosition);
        }

        private void CheckIfReachedDestination()
        {
            if (Vector3.Distance(_robotStateManager.transform.position, _targetPosition) < Data.ReachedThreshold)
            {
                SetNewTargetPosition();
            }
        }

        private void SearchForPlayer()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_robotStateManager.transform.position, Data.SearchRadius, Data.SearchMask);
            foreach (var hitCollider in hitColliders)
            {
                PlayerStateManager player = hitCollider.GetComponent<PlayerStateManager>();
                if (player != null)
                {
                    _robotStateManager.SwitchToAggroState(player);
                    break;
                }
            }
        }
    }
}