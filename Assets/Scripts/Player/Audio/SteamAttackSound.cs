using System;
using Player.Attack;
using Systems.Steam;
using UnityEngine;

namespace Player.Audio
{
    public class SteamAttackSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField] private SteamResourceHolder _holder;

        [SerializeField] private PlayerAttackManager _attackManager;


        [SerializeField] private float _volumeLerpSpeed = 5;

        [SerializeField] private float _blowPitch = 1;
        [SerializeField] private float _suckPitch = 1f;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }


        private void Update()
        {

            float targetVolume = 0;
            float targetPitch = 1;
            if (_attackManager.IsAttacking)
            {
                targetVolume = _holder.SteamFillPercent;
                
                switch (_attackManager.AttackState)
                {
                    case PlayerAttackManager.AttackTypes.Blow:
                        targetPitch = _blowPitch;
                        break;
                    case PlayerAttackManager.AttackTypes.Suck:
                        targetPitch = _suckPitch;
                        break;
                }
            }            
            
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, targetVolume, Time.deltaTime * _volumeLerpSpeed);
            _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, targetPitch, Time.deltaTime * _volumeLerpSpeed);
        }
    }
}