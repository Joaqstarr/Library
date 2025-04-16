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


        bool _originalPositionSet = false;
        public RobotIdleState(RobotStateManager robotStateManager) : base(robotStateManager)
        {
            _originalPositionSet = false;
            _originalPosition = _robotStateManager.transform.position;
        }

        protected override void OnEnterState()
        {
        }

        public override void OnUpdateState()
        {
            CheckIfFuelEmpty();

            CheckIfOriginalPositionSet();
            
            if (_originalPositionSet)
            {
                CheckIfReachedDestination();
            }

            Vector3 target = _targetPosition;
            if (_robotStateManager.transform.parent != null)
            {
                target = _robotStateManager.transform.parent.TransformPoint(target);
            }

            Agent.SetDestination(target);
         

            SearchForPlayer();
        }

        private void CheckIfOriginalPositionSet()
        {
            if (!_originalPositionSet)
            {
                if (Agent.isOnNavMesh)
                {
                    _originalPositionSet = true;
                    _originalPosition = _robotStateManager.transform.localPosition;
                    
                    SetNewTargetPosition();
                }
            }
        }


        protected override void OnExitState()
        {
        }

        private void SetNewTargetPosition()
        {
            Vector2 randomDirection = Random.insideUnitCircle * Data.WanderRadius;
            _targetPosition = _originalPosition + new Vector3(randomDirection.x, 0, randomDirection.y);
        }

        private void CheckIfReachedDestination()
        {
            float distance = Vector3.Distance(_robotStateManager.transform.localPosition, _targetPosition);
            if (distance < Data.ReachedThreshold)
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