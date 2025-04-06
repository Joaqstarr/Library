using UnityEngine;

namespace Level.MovingPlatform
{
    public class MovingPlatformAmbientNoise : MonoBehaviour
    {
        private MovingPlatformVelocityTransfer _movingPlatformBehavior;
        private AudioSource _audioSource;
        private void Awake()
        {
            _movingPlatformBehavior = GetComponent<MovingPlatformVelocityTransfer>();
            _audioSource = GetComponent<AudioSource>();

            _audioSource.time = Random.Range(0, _audioSource.clip.length);

        }

        private void Update()
        {
            float vel = _movingPlatformBehavior.CurrentDelta.magnitude / Time.deltaTime;

            _audioSource.volume = vel;
        }
    }
}