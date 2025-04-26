using System;
using DG.Tweening;
using UnityEngine;

namespace Player.Audio
{
    public class InsidePipeSoundEffect : MonoBehaviour
    {
        private AudioSource _audioSource;
        private float _maxVolume = 0;

        private Vector3 lastPos = Vector3.zero;

        private void Start()
        {
            lastPos = transform.position;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void EnterPipe()
        {
            _maxVolume = 1;
        }

        public void ExitPipeSoundEffect()
        {
            _maxVolume = 0;
        }

        private void Update()
        {
            if(_maxVolume == 0)
            {
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0, Time.deltaTime * 5);
                return;
            }
            float delta = (transform.position - lastPos).magnitude;
            
            if (delta > 0.1f)
            {
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, _maxVolume, Time.deltaTime * 5);
                lastPos = transform.position;
            }
            else
            {
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0, Time.deltaTime * 5);
            }
        }
    }
}