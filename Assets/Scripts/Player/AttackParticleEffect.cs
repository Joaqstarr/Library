using System;
using Player.Attack;
using Systems.Steam;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{
    public class AttackParticleEffect : MonoBehaviour
    {
        [SerializeField] private VisualEffect _suckEffect;
        [SerializeField] private VisualEffect _blowEffect;

        private PlayerAttackManager _playerAttackManager;
        private SteamResourceHolder _resourceHolder;

        private void Awake()
        {
            _resourceHolder = GetComponentInParent<SteamResourceHolder>();
            
            _playerAttackManager = GetComponentInParent<PlayerAttackManager>();
        }

        private void Update()
        {
            if(_playerAttackManager == null) return;

            if (_playerAttackManager.IsAttacking && _resourceHolder.SteamFillPercent > 0.02)
            {
                switch (_playerAttackManager.AttackState)
                {
                    case PlayerAttackManager.AttackTypes.Suck:
                        EnableEffect(_suckEffect);
                        DisableEffect(_blowEffect);
                        break;
                    case PlayerAttackManager.AttackTypes.Blow:
                        EnableEffect(_blowEffect);
                        DisableEffect(_suckEffect);
                        break;
                    default:
                        DisableEffect(_suckEffect);
                        DisableEffect(_blowEffect);
                        break;
                }

            }
            else
            {
                DisableEffect(_blowEffect);
                DisableEffect(_suckEffect);
            }
            
            
        }

        private void DisableEffect(VisualEffect effect)
        {
            if(effect && effect.HasAnySystemAwake())
            {
                effect.Stop();
            }
        }
        
        private void EnableEffect(VisualEffect effect)
        {
            if (effect && !effect.HasAnySystemAwake())
            {
                effect.Reinit(); 
                effect.Play(); 
            }
        }
    }
}