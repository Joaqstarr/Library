using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Robot
{
    public class RobotStrafeState : BaseAggroState
    {
        private float _strafeDistance = 3f;
        private float _strafeSpeed = 2f;


        private float _attackTimer = 0;
        public RobotStrafeState(RobotStateManager robot, RobotAggroState aggroState) : base(robot, aggroState)
        {
        }
        protected override void OnEnterState()
        {
            _attackTimer = 0;

            Agent.isStopped = true;
        }

        public override void OnUpdateState()
        {
            if(_aggroState.Player == null)
            {
                _robotStateManager.SwitchToIdleState();
                return;
            }
            if (_attackTimer > Data.AttackTime)
            {
                _aggroState.StartStompAttack();
                return;
            }
            else
            {
                _attackTimer += Time.deltaTime;
            }
            if (Vector3.Distance(_robotStateManager.transform.position, _aggroState.Player.transform.position) >
                Data.StrafeDistance * 1.2f)
            {
                _aggroState.SwitchToApproachState();
                return;
            }

        }
        protected override void OnExitState()
        {

        }        
    }
}