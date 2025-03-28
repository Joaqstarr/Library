using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Robot
{
    public class RobotIdleState : RobotBaseState
    {
        private Vector3 _originalPosition;
        private Vector3 _targetPosition;
        private float _wanderRadius = 5f;
        private float _reachedThreshold = 0.1f;

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
        }

        protected override void OnExitState()
        {
        }

        private void SetNewTargetPosition()
        {
            Vector2 randomDirection = Random.insideUnitCircle * _wanderRadius;
            _targetPosition = _originalPosition + new Vector3(randomDirection.x, 0, randomDirection.y);
            Agent.SetDestination(_targetPosition);
        }

        private void CheckIfReachedDestination()
        {
            if (Vector3.Distance(_robotStateManager.transform.position, _targetPosition) < _reachedThreshold)
            {
                SetNewTargetPosition();
            }
        }
    }
}