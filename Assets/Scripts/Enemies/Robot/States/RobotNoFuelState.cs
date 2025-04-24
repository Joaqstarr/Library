using UnityEngine;

namespace Enemies.Robot
{
    public class RobotNoFuelState : RobotBaseState
    {
        private static readonly int IsEmpty = Animator.StringToHash("IsEmpty");

        public RobotNoFuelState(RobotStateManager robotStateManager) : base(robotStateManager)
        {
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();

            _robotStateManager.EmptyObstacle.enabled = true;
            Agent.isStopped = true;
            Agent.enabled = false;
            //play shut down animation
            RobotAnimator.SetBool(IsEmpty, true);

            _robotStateManager.OnRobotCollisionEnter += OnRobotCollisionEnter;

            if (_robotStateManager.Data.IsKinematicByDefault)
            {
                _robotStateManager.Rigidbody.isKinematic = true;
            }
            else
            {
                _robotStateManager.Rigidbody.isKinematic = false;
            }
        }



        public override void OnUpdateState()
        {
            base.OnUpdateState();

            if (_robotStateManager.SteamTank.SteamFillPercent > 0.1f)
            {
                _robotStateManager.SwitchToIdleState();
            }
        }

        protected override void OnExitState()
        {
            base.OnExitState();
            _robotStateManager.EmptyObstacle.enabled = false;

            Agent.enabled = true;
            Agent.isStopped = false;
            RobotAnimator.SetBool(IsEmpty, false);
            _robotStateManager.OnRobotCollisionEnter -= OnRobotCollisionEnter;
            _robotStateManager.Rigidbody.isKinematic = true;

        }
        
        private void OnRobotCollisionEnter(Collider other)
        {
            if (other.CompareTag("StompRing"))
            {
                _robotStateManager.Rigidbody.isKinematic = false;

                Vector3 knockbackDirection = ((_robotStateManager.transform.position + (_robotStateManager.transform.up * 2)) - other.transform.position).normalized;
                float knockbackForce = 50f;
                _robotStateManager.Rigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}