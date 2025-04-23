using System;
using Player.Attack;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.Animation
{
    public class PlayerAttackLookTowardsTarget : MonoBehaviour
    {
        private PlayerAttackManager _playerAttackManager;
        private Rig _rig;
        private float _weightSpeed = 10;

        private void Awake()
        {
            _rig = GetComponent<Rig>();
            _playerAttackManager = GetComponentInParent<PlayerAttackManager>();
        }

        private void Update()
        {
            float target = 0;
            
            if (_playerAttackManager.IsAttacking)
            {
                target = 1;
            }
            
            if (_rig.weight != target)
            {
                _rig.weight = Mathf.Lerp(_rig.weight, target, Time.deltaTime * _weightSpeed);
            }
        }
    }
}