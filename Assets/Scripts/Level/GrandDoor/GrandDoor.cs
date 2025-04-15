using System;
using UnityEngine;
using Utility;
using Utility.SceneManagement;

namespace Level.GrandDoor
{
    public class GrandDoor : MonoBehaviour
    {
        [Serializable]
        public struct DoorLevelData
        {
            public SceneReference Level;
            public PlayerDetectingTrigger Trigger;            
        }
        
        [SerializeField] private DoorLevelData _hubLevel;
        [SerializeField] private DoorLevelData _gameLevel;
        
        [SerializeField]
        private PlayerDetectingTrigger _insideDoorTrigger;

        private enum LevelState
        {
            Hub,
            Game,
            Both
        }

        private LevelState _currentLevelLoadedState;
        
        private bool _doorOpen = false;
        private bool _awaitingPlayerEnter = true;

        public void OpenDoor()
        {
            //load level
            if (!_hubLevel.Level.IsLoaded())
            {
                _hubLevel.Level.LoadScene();
            }
            if(!_gameLevel.Level.IsLoaded())
            {
                _gameLevel.Level.LoadScene();
            }

            _currentLevelLoadedState = LevelState.Both;

            //play open animation

            _awaitingPlayerEnter = true;
        }

        public void CloseDoor()
        {
            //unload level

            switch (_currentLevelLoadedState)
            {
                case LevelState.Game:
                    _hubLevel.Level.UnloadScene();
                    break;
                case LevelState.Hub:
                    _gameLevel.Level.UnloadScene();
                    break;
            }
        }

        private void OnEnable()
        {
            _hubLevel.Trigger.OnPlayerEnter.AddListener(EnteredHub);
            _gameLevel.Trigger.OnPlayerEnter.AddListener(EnteredGameLevel);
            _insideDoorTrigger.OnPlayerEnter.AddListener(PlayerEnteredDoor);
        }




        private void OnDisable()
        {
            _hubLevel.Trigger.OnPlayerEnter.RemoveListener(EnteredHub);
        }
        
        private void PlayerEnteredDoor()
        {
            if (_currentLevelLoadedState == LevelState.Both)
            {
                _awaitingPlayerEnter = false;
            }

        }
        private void EnteredHub()
        {
            if (_currentLevelLoadedState != LevelState.Both || _awaitingPlayerEnter) return;
            
            _currentLevelLoadedState = LevelState.Hub;
            CloseDoor();
        }
        
        private void EnteredGameLevel()
        {
            if (_currentLevelLoadedState != LevelState.Both || _awaitingPlayerEnter) return;
            
            _currentLevelLoadedState = LevelState.Game;
            CloseDoor();
        }
    }
}