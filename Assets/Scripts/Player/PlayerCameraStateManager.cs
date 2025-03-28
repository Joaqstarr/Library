using System;
using UnityEngine;

namespace Player
{
    public class PlayerCameraStateManager : MonoBehaviour
    {
        private static readonly int IsAimingId = Animator.StringToHash("IsAiming");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public bool IsAiming
        {
            get => _animator.GetBool(IsAimingId);
            set => _animator.SetBool(IsAimingId, value);
        }
    }
}