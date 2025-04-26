using System;
using Audio;
using Cinemachine;
using Level.Pipe;
using Player.Attack;
using Player.Face;
using Player.States;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.Timeline;
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
        
        public PlayerCameraStateManager PlayerCameraStateManagerInstance{get; private set;}
        public PlayerAttackManager PlayerAttackManagerInstance{get; private set;}

        public FaceEmotionHandler FaceEmotionHandlerInstance{get; private set;}
        public Animator AnimatorInstance{get; private set;}
        public SignalReceiver PlayerSignalReceiverInstance{get; private set;}

        [field: SerializeField] public Transform PlayerArt { get; private set; }

        [field: Header("Camera and Aim Settings")]
        [field: SerializeField] public CinemachineVirtualCameraBase ThirdPersonCamera { get; private set; }
        [field: SerializeField] public CinemachineVirtualCameraBase AimCamera { get; private set; }
        [field: SerializeField] public Transform AimTransform { get; private set; }
        
        [field:SerializeField] public RandomClipPlayer EnterExitPipeSound { get; private set; }
        #region States
        
        private LocomotionState _locomotionState;
        private PipeState _pipeState;
        private CinematicState _cinematicState;

        #endregion

        
        
        
        private void Awake()
        {
            PlayerAttackManagerInstance = GetComponent<PlayerAttackManager>();
            PlayerCloudAttractionHandlerInstance = GetComponent<PlayerCloudAttractionHandler>();
            PlayerMovementInstance = GetComponent<PlayerMovement>();
            PlayerControlsInstance = GetComponent<PlayerControls>();
            PlayerInteractionManagerInstance = GetComponent<PlayerInteractionManager>();
            PlayerCameraStateManagerInstance = GetComponentInChildren<PlayerCameraStateManager>();
            FaceEmotionHandlerInstance = GetComponentInChildren<FaceEmotionHandler>();
            AnimatorInstance = GetComponent<Animator>();
            PlayerSignalReceiverInstance = GetComponent<SignalReceiver>();
            //state setup
            _locomotionState = new LocomotionState(this);
            _pipeState = new PipeState(this);
            _cinematicState = new CinematicState(this);

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

        public void SwitchToCinematicState()
        {
            _stateMachine.SwitchState(_cinematicState);
        }
    }
}