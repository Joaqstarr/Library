using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Robot
{
    public class RobotApproachState : BaseAggroState
    {

        public RobotApproachState(RobotStateManager robot, RobotAggroState aggroState) : base(robot, aggroState)
        {
        }

        protected override void OnEnterState()
        {
            Agent.isStopped = false;

            Agent.SetDestination(_aggroState.Player.transform.position);
        }

        
        public override void OnUpdateState()
        {
            if(_aggroState.Player == null)
            {
                _robotStateManager.SwitchToIdleState();
                return;
            }
            if (!_aggroState.Player)
            {
                _robotStateManager.SwitchToIdleState();
            }
            if (Vector3.Distance(_robotStateManager.transform.position, _aggroState.Player.transform.position) <= Data.StrafeDistance)
            {
                _aggroState.SwitchToStrafeState();
            }
            else
            {
                Agent.SetDestination(_aggroState.Player.transform.position);
            }
        }
    }
}