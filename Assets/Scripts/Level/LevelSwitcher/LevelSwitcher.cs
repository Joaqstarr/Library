using System;
using UnityEngine;
using Utility.SceneManagement;

namespace Level.LevelSwitcher
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private SceneReference[] _levels;

        private int _currentLevel = -1;
        
        public delegate void LevelChangedSignature(int levelIndex, SceneReference level);
        public static LevelChangedSignature OnLevelChanged;
        
        [SerializeField]
        private float _levelSwitchingDelay = 1f;
        private float _levelSwitchCooldown = 0f;

        private void Update()
        {
            _levelSwitchCooldown -= Time.deltaTime;
        }

        public void NextLevel()
        {
            if (_levels.Length == 0 || _levelSwitchCooldown > 0) return;
            _levelSwitchCooldown = _levelSwitchingDelay;

            _currentLevel = (_currentLevel + 1)%_levels.Length;
            OnLevelChanged?.Invoke(_currentLevel, _levels[_currentLevel]);
        }

        public void PreviousLevel()
        {
            if (_levels.Length == 0 || _levelSwitchCooldown > 0) return;
            
            
            _levelSwitchCooldown = _levelSwitchingDelay;
            _currentLevel = (_currentLevel - 1)%_levels.Length;
            
            OnLevelChanged?.Invoke(_currentLevel, _levels[_currentLevel]);

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