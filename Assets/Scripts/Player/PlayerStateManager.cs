using System;
using Player.States;
using UnityEngine;
using Utility.StateMachine;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        private HierarchalStateMachine _stateMachine;
        
        public PlayerControls PlayerControlsInstance{get; private set;}
        public PlayerMovement PlayerMovementInstance{get; private set;}

        #region States
        
        private LocomotionState _locomotionState;
        private PipeState _pipeState;

        #endregion
        
        private void Awake()
        {
            PlayerMovementInstance = GetComponent<PlayerMovement>();
            PlayerControlsInstance = GetComponent<PlayerControls>();
            
            
            //state setup
            _locomotionState = new LocomotionState(this);
            _pipeState = new PipeState(this);
            
            _stateMachine = new HierarchalStateMachine();
        }

        private void Start()
        {
            SwitchToLocomotionState();
        }

        public void SwitchToLocomotionState()
        {
            SwitchState(_locomotionState);
        }

        private void Update()
        {
            _stateMachine?.OnUpdateState();
        }

        private void FixedUpdate()
        {
            _stateMachine?.OnFixedUpdateState();
        }

        private void SwitchState(BaseState newState)
        {
            _stateMachine.SwitchState(newState);
        }
    }
}