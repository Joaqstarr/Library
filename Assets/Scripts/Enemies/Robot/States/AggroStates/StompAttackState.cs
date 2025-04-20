using Enemies.Robot.Attacks;
using Systems.CombatTokens;
using UnityEngine;

namespace Enemies.Robot
{
    public class StompAttackState : BaseAggroState
    {
        private static readonly int Stomp = Animator.StringToHash("Stomp");
        private StompRing _stompRing;
        private CombatTokenManager.Token _attackToken;
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

            _attackToken = null;
            if (CombatTokenManager.Instance)
            {
                _attackToken = CombatTokenManager.Instance.RequestToken(_robotStateManager.gameObject);
                if (_attackToken == null)
                {
                    _aggroState.SwitchToApproachState();
                    return;
                }
            }
            
            Agent.isStopped = true;
            
            AnimationEventHandler.OnAnimationEnded += AnimationEventHandlerOnOnAnimationEnded;
            AnimationEventHandler.OnAttackHit += OnStompAnimationEvent;

            //play stomp animation
            //subscribe to animation event
            RobotAnimator.SetTrigger(Stomp);

            
        }

        private void AnimationEventHandlerOnOnAnimationEnded()
        {
            _aggroState.SwitchToApproachState();
        }

        protected override void OnExitState()
        {
            base.OnExitState();

            if (_attackToken != null)
            {
                _attackToken.DeallocateToken();
                _attackToken = null;
            }
            
            AnimationEventHandler.OnAnimationEnded -= AnimationEventHandlerOnOnAnimationEnded;
            AnimationEventHandler.OnAttackHit -= OnStompAnimationEvent;

            Agent.isStopped = false;
            //unsubscribe from animation event
        }
        
        
        private void OnStompAnimationEvent()
        {
            //set to foot position
            _stompRing.StartStompRing(_robotStateManager.RightFoot.position);
        }
        
        
    }
}