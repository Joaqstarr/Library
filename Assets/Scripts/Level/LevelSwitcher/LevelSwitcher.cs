using UnityEngine;
using Utility.SceneManagement;

namespace Level.LevelSwitcher
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private SceneReference[] _levels;

        private int _currentLevel = -1;
        
        public void NextLevel()
        {
            if (_levels.Length == 0) return;
            
            if (_currentLevel >= 0)
            {
                _levels[_currentLevel].UnloadScene();
            }
            
            _currentLevel = (_currentLevel + 1)%_levels.Length;
            
            _levels[_currentLevel].LoadScene();
        }

        public void PreviousLevel()
        {
            if (_levels.Length == 0) return;

            if (_currentLevel >= 0)
            {
                _levels[_currentLevel].UnloadScene();
            }
            

            _currentLevel = (_currentLevel - 1)%_levels.Length;
            
            _levels[_currentLevel].LoadScene();

        }
    }
}