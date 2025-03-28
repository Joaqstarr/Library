using System;
using Cinemachine;
using Level.Pipe;
using Player.States;
using UnityEngine;
using UnityEngine.Splines;
using Utility.StateMachine;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        private HierarchalStateMachine _stateMachine;
        
        public PlayerControls PlayerControlsInstance{get; private set;}
        public PlayerMovement PlayerMovementInstance{get; private set;}
        public PlayerCloudAttractionHandler PlayerCloudAttractionHandlerInstance{get; private set;}
        public PlayerInteractionManager PlayerInteractionManagerInstance{get; private set;}
        
        [field: SerializeField] public CinemachineVirtualCameraBase ThirdPersonCamera { get; private set; }
        [field: SerializeField] public CinemachineVirtualCameraBase AimCamera { get; private set; }
        #region States
        
        private LocomotionState _locomotionState;
        private PipeState _pipeState;

        #endregion
        
        private void Awake()
        {
            PlayerCloudAttractionHandlerInstance = GetComponent<PlayerCloudAttractionHandler>();
            PlayerMovementInstance = GetComponent<PlayerMovement>();
            PlayerControlsInstance = GetComponent<PlayerControls>();
            PlayerInteractionManagerInstance = GetComponent<PlayerInteractionManager>();
            
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

        public void SwitchToPipeState(Pipe pipe, float entrancePosition)
        {
            _pipeState.SetPositionInPipe(entrancePosition);
            _pipeState.SetPipe(pipe);
            SwitchState(_pipeState);
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