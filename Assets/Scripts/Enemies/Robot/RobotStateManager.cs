using Player;
using UnityEngine;
using UnityEngine.AI;
using Utility.StateMachine;

namespace Enemies.Robot
{
    public class RobotStateManager : MonoBehaviour
    {

        
        public NavMeshAgent Agent { get; private set; }
        
        
        #region States
        private HierarchalStateMachine _stateMachine;

        private RobotBaseState _idleState;
        private RobotAggroState _aggroState;

        #endregion
        
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            
            // Initialize with a default state
            _idleState = new RobotIdleState(this);
            _aggroState = new RobotAggroState(this);
        }

        private void Update()
        {
            _stateMachine.OnUpdateState();
        }

        private void FixedUpdate()
        {
            _stateMachine.OnFixedUpdateState();
        }

        public void SwitchState(RobotBaseState newState)
        {
            _stateMachine.SwitchState(newState);
        }

        public void SwitchToIdleState()
        {
            SwitchState(_idleState);
        }

        public void SwitchToAggroState(PlayerStateManager player)
        {
            _aggroState.SetPlayerTarget(player);
            SwitchState(_aggroState);
        }
    }
}