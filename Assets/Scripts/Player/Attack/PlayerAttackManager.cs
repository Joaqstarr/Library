using System;
using Systems.Steam;
using UnityEngine;

namespace Player.Attack
{
    public class PlayerAttackManager : MonoBehaviour
    {
        public enum AttackTypes
        {
            Suck,
            Blow
        }
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
        
        private void Suck()
        {
            int count = _attackHitbox.SteamHoldersInRange.Count;

            foreach (var holder in _attackHitbox.SteamHoldersInRange)
            {
                holder.BeginSteamTransferTo(_playerSteamResource, Time.deltaTime * _resourceTransferRate/count);
            }
        }
        
        private void Blow()
        {
            int count = _attackHitbox.SteamHoldersInRange.Count;
            
            foreach (var holder in _attackHitbox.SteamHoldersInRange)
            {
                holder.BeginSteamTransferFrom(_playerSteamResource, Time.deltaTime * _resourceTransferRate/count);
            }
        }

        private void Update()
        {
            if(_playerControls.AttackPressed)
            {
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
            }
            else
            {
                AttackState = AttackTypes.Blow;
            }
        }
    }
}