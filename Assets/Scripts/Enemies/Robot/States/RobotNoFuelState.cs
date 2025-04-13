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

            Agent.isStopped = true;
            Agent.enabled = false;
            //play shut down animation
            RobotAnimator.SetBool(IsEmpty, true);

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
            Agent.isStopped = false;
            Agent.enabled = true;
            RobotAnimator.SetBool(IsEmpty, false);

            base.OnExitState();
        }
    }
}