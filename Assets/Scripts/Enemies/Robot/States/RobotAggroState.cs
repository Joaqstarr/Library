using Player;
using UnityEngine;

namespace Enemies.Robot
{
    public class RobotAggroState : RobotBaseState
    {
        private PlayerStateManager _player;
        
        public RobotAggroState(RobotStateManager robot) : base(robot)
        {
        }
        
        public void SetPlayerTarget(PlayerStateManager player)
        {
            this._player = player;
        }
    }

}