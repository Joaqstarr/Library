using System;
using UnityEngine;
using UnityEngine.Playables;
using Utility.SceneManagement;

namespace Level.LevelSwitcher
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private SceneReference[] _levels;

        private int _currentLevel = -1;
        
        public delegate void LevelChangedSignature(int levelIndex, SceneReference level);
        public static LevelChangedSignature OnLevelChanged;
        
        public delegate SceneReference CurrentLevelRequestSignature();
        public static CurrentLevelRequestSignature RequestCurrentLevel;

        public delegate bool CanChangeLevelSignature();
        public static CanChangeLevelSignature CanChangeLevel;
        
        [SerializeField]
        private float _levelSwitchingDelay = 1f;
        private float _levelSwitchCooldown = 0f;


        private PlayableDirector _playableDirector;

        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();
        }

        private void Start()
        {
            if(RequestCurrentLevel != null)
            {
                SceneReference level = RequestCurrentLevel();

                if (level != null)
                {
                    for (int i = 0; i < _levels.Length; i++)
                    {
                        if (_levels[i].Equals(level))
                        {
                            _currentLevel = i;
                            OnLevelChanged?.Invoke(_currentLevel, _levels[_currentLevel]);
                            break;
                        }
                    }
                }
            }
        }

        private void Update()
        {
            _levelSwitchCooldown -= Time.deltaTime;
        }

        public void NextLevel()
        {
            if (_levels.Length == 0 || _levelSwitchCooldown > 0) return;
            if(CanChangeLevel != null && !CanChangeLevel()) return;
            
            _levelSwitchCooldown = _levelSwitchingDelay;

            _currentLevel = (_currentLevel + 1)%_levels.Length;
            OnLevelChanged?.Invoke(_currentLevel, _levels[_currentLevel]);
            _playableDirector.Play();

        }

        public void PreviousLevel()
        {
            if (_levels.Length == 0 || _levelSwitchCooldown > 0) return;
            if(CanChangeLevel != null && !CanChangeLevel()) return;
            
            _levelSwitchCooldown = _levelSwitchingDelay;
            _currentLevel = (_currentLevel - 1)%_levels.Length;
            
            OnLevelChanged?.Invoke(_currentLevel, _levels[_currentLevel]);

            _playableDirector.Play();
        }


        public int GetActiveLevelIndex()
        {
            return _currentLevel;
        }
        
        public int GetLevelCount()
        {
            return _levels.Length;
        }
    }
}