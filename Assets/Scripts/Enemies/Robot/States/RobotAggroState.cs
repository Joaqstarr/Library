using Player;
using UnityEngine;

namespace Enemies.Robot
{
    public class RobotAggroState : RobotBaseState
    {
        public PlayerStateManager Player { get; private set; }
        private float _stopDistance = 5f;
        #region States

        private RobotApproachState _approachState;
        private RobotStrafeState _strafeState;
        

        #endregion
        public RobotAggroState(RobotStateManager robot) : base(robot)
        {
            _approachState = new RobotApproachState(robot, this);
            _strafeState = new RobotStrafeState(robot, this);
        }
        
        public void SetPlayerTarget(PlayerStateManager player)
        {
            this.Player = player;
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            SwitchToApproachState();

        }

        public void SwitchToApproachState()
        {
            SwitchState(_approachState);
        }
        public void SwitchToStrafeState()
        {
            SwitchState(_strafeState);
        }
        
    }

}