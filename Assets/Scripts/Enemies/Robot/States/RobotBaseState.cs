using UnityEngine;
using UnityEngine.AI;
using Utility.StateMachine;

namespace Enemies.Robot
{
    public abstract class RobotBaseState : HierarchalStateMachine
    {
        protected RobotStateManager _robotStateManager;
        protected NavMeshAgent Agent;
        protected RobotBaseState(RobotStateManager robotStateManager)
        {
            _robotStateManager = robotStateManager;
            Agent = robotStateManager.Agent;
        }
        
    }
}