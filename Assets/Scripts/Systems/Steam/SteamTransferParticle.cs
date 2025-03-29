using System;
using UnityEngine;

namespace Systems.Steam
{
    [RequireComponent(typeof(ParticleSystem))]
    public class SteamTransferParticle : MonoBehaviour
    {
        [SerializeField] private SteamResourceHolder _steamResourceHolder;
        private ParticleSystem _particleSystem;

        [SerializeField] private float _cooldown = 0.5f;
        private float _cooldownTimer = 0;
        [SerializeField] private float _sizeModifier = 40f;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            _steamResourceHolder.OnInstantTransferFromBegin += OnInstantTransferFromBegin;
        }



        private void OnDisable()
        {
            _steamResourceHolder.OnInstantTransferFromBegin -= OnInstantTransferFromBegin;
        }

        private void Update()
        {
            _cooldownTimer -= Time.deltaTime;
        }

        private void OnInstantTransferFromBegin(float amount, bool visuals, SteamResourceHolder holder)
        {

            if (visuals && amount > 0.01 && _cooldownTimer < 0)
            {
                _cooldownTimer = _cooldown;
                
                var emitParams = new ParticleSystem.EmitParams
                {
                    position = holder.transform.position,
                    startSize = _sizeModifier * amount * _particleSystem.main.startSize.constant
                };
                _particleSystem.Emit(emitParams, 1);
            }
        }
    }
}