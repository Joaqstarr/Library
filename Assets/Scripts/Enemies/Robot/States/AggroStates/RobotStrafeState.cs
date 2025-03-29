using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Robot
{
    public class RobotStrafeState : BaseAggroState
    {
        private float _strafeDistance = 3f;
        private float _strafeSpeed = 2f;


        public RobotStrafeState(RobotStateManager robot, RobotAggroState aggroState) : base(robot, aggroState)
        {
        }
        protected override void OnEnterState()
        {
        }

        public override void OnUpdateState()
        {
            if (Vector3.Distance(_robotStateManager.transform.position, _aggroState.Player.transform.position) >
                Data.StrafeDistance * 1.2f)
            {
                _aggroState.SwitchToApproachState();
                return;
            }

            Vector3 direction = (_robotStateManager.transform.position - _aggroState.Player.transform.position).normalized;
            Vector3 strafeDirection = Vector3.Cross(direction, Vector3.up).normalized;
            Vector3 strafePosition = _aggroState.Player.transform.position + direction * _strafeDistance + strafeDirection * Mathf.Sin(Time.time * _strafeSpeed);

            Agent.SetDestination(strafePosition);
        }
        
    }
}