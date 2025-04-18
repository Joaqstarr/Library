﻿using Player;
using Systems.Steam;
using UnityEngine;
using UnityEngine.AI;
using Utility.StateMachine;

namespace Enemies.Robot
{
    public class RobotStateManager : MonoBehaviour
    {

        [field: SerializeField] public RobotData Data { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Animator RobotAnimator { get; private set; }
        public RobotAnimationEventHandler AnimationEventHandler { get; private set; }
        public SteamResourceHolder SteamTank { get; private set; }

        
        #region States
        private HierarchalStateMachine _stateMachine;

        private RobotBaseState _idleState;
        private RobotAggroState _aggroState;
        private RobotNoFuelState _noFuelState;
        private SpawnState _spawnState;
        #endregion
        
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            RobotAnimator = GetComponent<Animator>();
            AnimationEventHandler = GetComponent<RobotAnimationEventHandler>();
            SteamTank = GetComponentInChildren<SteamResourceHolder>();
            // Initialize with a default state
            _stateMachine = new HierarchalStateMachine();
            _idleState = new RobotIdleState(this);
            _aggroState = new RobotAggroState(this);
            _noFuelState = new RobotNoFuelState(this);
            _spawnState = new SpawnState(this);
            SwitchToIdleState();
        }

        private void Update()
        {
            _stateMachine.OnUpdateState();
        }

        private void FixedUpdate()
        {
            _stateMachine.OnFixedUpdateState();
        }

        public void SwitchState(RobotBaseState newState)
        {
            _stateMachine.SwitchState(newState);
        }

        public void SwitchToIdleState()
        {
            SwitchState(_idleState);
        }

        public void SwitchToAggroState(PlayerStateManager player)
        {
            _aggroState.SetPlayerTarget(player);
            SwitchState(_aggroState);
        }

        public void SwitchToNoFuelState()
        {
            SwitchState(_noFuelState);
        }


        public void PlaySpawnAnimation()
        {
            SwitchState(_spawnState);
        }
        
        
    }
}