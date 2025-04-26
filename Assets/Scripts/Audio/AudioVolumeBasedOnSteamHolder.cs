using System;
using Systems.Steam;
using UnityEngine;

namespace Audio
{
    public class AudioVolumeBasedOnSteamHolder : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField] private SteamResourceHolder _holder;
        
        [SerializeField]
        private float _volumeLerpSpeed = 5f;

        private void Update()
        {
            if(_holder == null || _audioSource == null) return;
            
            float targetVolume = _holder.SteamFillPercent;
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, targetVolume, Time.deltaTime * _volumeLerpSpeed);
        }
    }
}