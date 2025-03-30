using Enemies.Robot.Attacks;
using UnityEngine;

namespace Enemies.Robot
{
    public class StompAttackState : BaseAggroState
    {
        private StompRing _stompRing;
        
        public StompAttackState(RobotStateManager robotStateManager, RobotAggroState aggroState) : base(robotStateManager, aggroState)
        {

        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            if (_stompRing == null)
            {
                _stompRing = GameObject.Instantiate(Data.StompRingPrefab, _robotStateManager.transform);
                _stompRing.gameObject.SetActive(false);
            }
            Agent.isStopped = true;
            
            //play stomp animation
            //subscribe to animation event
            
            //debug just call stomp
            OnStompAnimationEvent();
        }

        protected override void OnExitState()
        {
            base.OnExitState();
        
            Agent.isStopped = false;
            //unsubscribe from animation event
        }
        
        
        private void OnStompAnimationEvent()
        {
            //set to foot position
            _stompRing.StartStompRing(_robotStateManager.transform.position);
            _aggroState.SwitchToApproachState();
        }
        
        
    }
}