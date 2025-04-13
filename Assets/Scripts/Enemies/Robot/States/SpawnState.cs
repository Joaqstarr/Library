using DG.Tweening;
using UnityEngine;

namespace Enemies.Robot
{
    public class SpawnState : RobotBaseState
    {
        private float _height = 20f;
        public SpawnState(RobotStateManager robotStateManager) : base(robotStateManager)
        {
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            _robotStateManager.Agent.enabled = false;

            Vector3 startLoc = _robotStateManager.transform.localPosition;
            _robotStateManager.transform.localPosition = _robotStateManager.transform.localPosition + Vector3.up * _height;

            _robotStateManager.transform.DOLocalMove(startLoc, _height / Physics.gravity.magnitude, false).SetEase(Ease.InExpo).onComplete +=
                () =>
                {
                    _robotStateManager.SwitchToIdleState();
                };

        }

        protected override void OnExitState()
        {
            base.OnExitState();
            
            _robotStateManager.Agent.enabled = true;
        }
    }
}