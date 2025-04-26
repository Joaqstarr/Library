using UnityEngine;

namespace Audio
{
    public class RandomClipPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _audioClips;
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField]private Vector2 _minMaxPitch = new Vector2(0.8f, 1.2f);
        [SerializeField]private Vector2 _minMaxVolune = new Vector2(0.8f, 1.2f);

        public void PlayRanClip()
        {
            if (_audioClips.Length == 0)
            {
                Debug.LogWarning("No audio clips assigned.", gameObject);
                return;
            }
            AudioClip clip = _audioClips[Random.Range(0, _audioClips.Length)];
            
            if (_audioSource != null)
            {
                _audioSource.volume = Random.Range(_minMaxVolune.x, _minMaxVolune.y);
                _audioSource.pitch = Random.Range(_minMaxPitch.x, _minMaxPitch.y);
                _audioSource.clip = clip;
                _audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource is not assigned.", gameObject);
            }
        }
    }
}