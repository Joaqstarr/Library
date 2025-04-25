using System;
using Systems.Gamemode;
using UnityEngine;

namespace Systems.AmbientSound
{
    public class HubMusicManager : MonoBehaviour
    {
        [Serializable]
        private struct HubMusicParts
        {
            public AudioClip _intro;
            public AudioClip _loop;
        }
        
        [SerializeField] private HubMusicParts[] _hubMusicParts;
        private bool _startedPlaying = false;
        private AudioSource _audioSource;

        private HubMusicParts _currentLevel;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (Gamemanager.Instance)
            {
                int level = Gamemanager.Instance.GetSaveData().GetLevelCompletedCount();
                
                _currentLevel = _hubMusicParts[level % _hubMusicParts.Length];
            }
            else
            {
                _currentLevel = _hubMusicParts[0];
            }
        }

        private void Update()
        {
            if (!_startedPlaying)
            {
                if (_audioSource.volume > 0.5f)
                {
                    StartPlayingMusic();
                }
            }
            else
            {
                if(!_audioSource.loop && _audioSource.time / _audioSource.clip.length > 0.95f)
                {
                    _audioSource.clip = _currentLevel._loop;
                    _audioSource.loop = true;
                    _audioSource.Play();
                }
            }
        }

        private void StartPlayingMusic()
        {
            _startedPlaying = true;
            _audioSource.clip = _currentLevel._intro;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }
}