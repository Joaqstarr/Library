using System;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        private static readonly int GroundedParam = Animator.StringToHash("Grounded");
        private static readonly int SpeedParam = Animator.StringToHash("Speed");
        private static readonly int JumpTrigger = Animator.StringToHash("Jump");
        private static readonly int LaunchTrigger = Animator.StringToHash("Launch");


        private Animator _animator;
        [SerializeField] private float _smoothSpeed = 2f;

        public float MoveSpeed
        {
            set => _targMoveSpeed = value;
            get => _targMoveSpeed;
        }

        private float MoveSpeedAnimator
        {
            set => _animator.SetFloat(SpeedParam,Mathf.Clamp01( value));
            get => _animator.GetFloat(SpeedParam);
        }
        public bool Grounded
        {
            get => _animator.GetBool(GroundedParam);
            set => _animator.SetBool(GroundedParam, value);
        }

        private float _targMoveSpeed;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            MoveSpeedAnimator = Mathf.Lerp(MoveSpeedAnimator, MoveSpeed, Time.deltaTime * _smoothSpeed);
        }

        public void SetJumpTrigger()
        {
            _animator.SetTrigger(JumpTrigger);
        }

        public void SetLaunchTrigger()
        {
            _animator.SetTrigger(LaunchTrigger);
        }
    }
}