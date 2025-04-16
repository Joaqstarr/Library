using System;
using Systems.Gamemode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;
using Utility.SceneManagement;

namespace Level.GrandDoor
{
    public class GrandDoor : MonoBehaviour
    {
        private static readonly int IsDoorOpen = Animator.StringToHash("IsDoorOpen");

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

        private Animator _animator;
        private enum LevelState
        {
            Hub,
            Game,
            Both
        }

        private LevelState _currentLevelLoadedState;
        
        private bool _doorOpen = false;
        private bool _awaitingPlayerEnter = true;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentLevelLoadedState = LevelState.Hub;
        }

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
            SceneManager.sceneLoaded += SceneLoaded;

        }

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == _hubLevel.Level.ScenePath || arg0.name == _gameLevel.Level.ScenePath)
            {
                _animator.SetBool(IsDoorOpen, true);
                _doorOpen = true;
                _awaitingPlayerEnter = true;
            }
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        public void CloseDoor()
        {
            //unload level

            _animator.SetBool(IsDoorOpen, false);
            _doorOpen = false;

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
            
            if (Gamemanager.Instance)
            {
                Gamemanager.Instance.SetCurrentLevel(_hubLevel.Level);
            }
            CloseDoor();
        }
        
        private void EnteredGameLevel()
        {
            if (_currentLevelLoadedState != LevelState.Both || _awaitingPlayerEnter) return;
            
            _currentLevelLoadedState = LevelState.Game;
            if (Gamemanager.Instance)
            {
                Gamemanager.Instance.SetCurrentLevel(_gameLevel.Level);
            }
            CloseDoor();
        }

        public void OnDoorClosed()
        {
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
    }
}