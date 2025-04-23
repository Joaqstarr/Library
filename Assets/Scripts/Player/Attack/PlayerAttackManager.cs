using System;
using Systems.Steam;
using UnityEngine;

namespace Player.Attack
{
    public class PlayerAttackManager : MonoBehaviour
    {

        public delegate void AttackStateDel(bool isInhaling);
        public static AttackStateDel OnAttackStateChange;
        public static AttackStateDel OnAttackStart;
        public static AttackStateDel OnAttackEnd;
        
        
        public enum AttackTypes
        {
            Suck,
            Blow
        }

        public bool IsAttacking { get; private set; } = false;
        public AttackTypes AttackState { get; private set; }
        private Hitbox _attackHitbox;
        private SteamResourceHolder _playerSteamResource;
        private PlayerControls _playerControls;
        
        [SerializeField] private float _resourceTransferRate = 4;
        private void Awake()
        {
            _playerControls = GetComponent<PlayerControls>();
            _playerSteamResource = GetComponent<SteamResourceHolder>();
            _attackHitbox = GetComponentInChildren<Hitbox>();
        }

        
        private void Start()
        {
            OnAttackStateChange?.Invoke(true);
        }

        private void Suck()
        {
            int count = _attackHitbox.SteamHoldersInRange.Count;

            foreach (var holder in _attackHitbox.SteamHoldersInRange)
            {
                if (holder && holder != _playerSteamResource)
                {
                    holder.BeginSteamTransferTo(_playerSteamResource, Time.deltaTime * _resourceTransferRate/count, 0 , true);
                }
            }
        }
        
        private void Blow()
        {
            int count = _attackHitbox.SteamHoldersInRange.Count;
            
            foreach (var holder in _attackHitbox.SteamHoldersInRange)
            {
                if (holder && holder != _playerSteamResource)
                {
                    holder.BeginSteamTransferFrom(_playerSteamResource, Time.deltaTime * (_resourceTransferRate / count),
                        0, true);
                }
            }
        }

        private void Update()
        {
            if(_playerControls.AttackPressed)
            {
                if (!IsAttacking)
                {
                    OnAttackStart?.Invoke(AttackState == AttackTypes.Suck);
                }
                IsAttacking = true;
                switch (AttackState)
                {
                    case AttackTypes.Blow:
                        Blow();
                        break;
                    case AttackTypes.Suck:
                        Suck();
                        break;
                }
            }
            else
            {
                if (IsAttacking)
                {
                    OnAttackEnd?.Invoke(AttackState == AttackTypes.Suck);
                }
                IsAttacking = false;
            }
        }

        private void OnEnable()
        {
            _playerControls.OnTogglePressed += ToggleAttackType;
        }

        private void OnDisable()
        {
            _playerControls.OnTogglePressed -= ToggleAttackType;
        }

        private void ToggleAttackType()
        {
            if (AttackState == AttackTypes.Blow)
            {
                AttackState = AttackTypes.Suck;
                OnAttackStateChange?.Invoke(true);
            }
            else
            {
                AttackState = AttackTypes.Blow;
                OnAttackStateChange?.Invoke(false);

            }
        }
    }
}