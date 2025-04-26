using System;
using Player;
using Player.Attack;
using Systems.Gamemode;
using UnityEngine;

namespace UI.SteamFillBar
{
    public class SteamFillBarStatus : MonoBehaviour
    {
        private PlayerAttackManager _attackManager;

        public float Speed
        {
            get;
            private set;
        }
        [SerializeField]
        private float _speed = 100;

        private float _targetSpeed;

        [SerializeField]
        private float _smoothSpeed = 5;

        private CanvasGroup _canvasGroup;
        private void Start()
        {
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            _targetSpeed = _speed;
        }

        private void OnEnable()
        {
            Gamemanager.OnPlayerSpawned += OnPlayerSpawned;
        }
        
        private void OnDisable()
        {
            Gamemanager.OnPlayerSpawned -= OnPlayerSpawned;
        }
        
        private void OnPlayerSpawned(PlayerStateManager player)
        {
            _attackManager = player.GetComponent<PlayerAttackManager>();
        }
        
        private void Update()
        {
            float targAlpha = 0;
            if (_attackManager != null)
            {
                PlayerAttackManager.AttackTypes attackState = _attackManager.AttackState;
                // Update your UI element here with the fill amount
                // For example: steamFillBar.fillAmount = fillAmount;

                if (_attackManager.IsAttacking)
                {
                    targAlpha = 1;
                }
                if (attackState == PlayerAttackManager.AttackTypes.Suck)
                {
                    _targetSpeed = -_speed;
                    
                }
                else
                {
                    _targetSpeed = _speed;
                }
            }

            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, targAlpha, Time.deltaTime * _smoothSpeed);
            Speed = Mathf.Lerp(Speed, _targetSpeed, Time.deltaTime * _smoothSpeed);
        }
        
        
    }
}