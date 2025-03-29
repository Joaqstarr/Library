namespace Enemies.Robot
{
    public class BaseAggroState : RobotBaseState
    {
        protected RobotAggroState _aggroState;
        

        public BaseAggroState(RobotStateManager robotStateManager, RobotAggroState aggroState) : base(robotStateManager)
        {
            _aggroState = aggroState;
        }
    }
}