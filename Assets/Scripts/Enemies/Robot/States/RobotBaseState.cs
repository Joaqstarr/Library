using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Utility.StateMachine;

namespace Enemies.Robot
{
    public abstract class RobotBaseState : HierarchalStateMachine
    {
        protected RobotStateManager _robotStateManager;
        protected NavMeshAgent Agent;
        protected RobotData Data;
        protected Animator RobotAnimator;
        protected RobotAnimationEventHandler AnimationEventHandler;
        protected RobotBaseState(RobotStateManager robotStateManager)
        {
            _robotStateManager = robotStateManager;
            Agent = robotStateManager.Agent;
            Data = robotStateManager.Data;
            RobotAnimator = robotStateManager.RobotAnimator;
            AnimationEventHandler = robotStateManager.AnimationEventHandler;
        }
        
        protected void CheckIfFuelEmpty()
        {
            if (_robotStateManager.SteamTank.SteamFillPercent <= 0)
            {
                _robotStateManager.SwitchToNoFuelState();
            }
        }
    }
}