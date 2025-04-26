using System;
using Player;
using Systems.Gamemode;
using UnityEngine;
using UnityEngine.Rendering;

namespace Systems.AmbientSound
{
    public class VolumeToLocalPostProcess : MonoBehaviour
    {
        [SerializeField] private Volume _postProcessVolume;
        
        [SerializeField]
        private AudioSource[] _audioSources;

        private Collider _collider;
        PlayerStateManager _player;
        private void Awake()
        {
            if (_postProcessVolume)
            {
                _collider = _postProcessVolume.GetComponent<Collider>();
            }
        }

        private void Start()
        {
            if (Gamemanager.Instance)
            {
                _player = Gamemanager.Instance.GetPlayer();
            }
            else
            {
                _player = FindObjectOfType<PlayerStateManager>();
            }
        }

        private void Update()
        {
            if(!_postProcessVolume || _audioSources == null || _audioSources.Length == 0)
                return;


            if (Gamemanager.Instance && !_player)
            {
                _player = Gamemanager.Instance.GetPlayer();
            }
            
            if (_player )
            {
             
                float weight = GetBlendWeight(_player.transform);

                foreach (var source in _audioSources)
                {
                    source.volume = weight;
                }
            }
        }
        
        float GetBlendWeight(Transform player)
        {
            if (_collider.bounds.Contains(player.position))
                return 1f;

            float distance = Vector3.Distance(player.position, _collider.ClosestPoint(player.position));
            float blendDistance = _postProcessVolume.blendDistance;

            return Mathf.Clamp01(1f - (distance / blendDistance));
        }

        
        
    }
}