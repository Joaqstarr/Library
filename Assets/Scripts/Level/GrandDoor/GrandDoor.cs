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
            Game
        }
        
        
        
        private bool _doorOpen = false;
        private bool _awaitingPlayerEnter = false;

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
            
            //play open animation

            _awaitingPlayerEnter = true;
        }

        public void CloseDoor()
        {
            //unload level
        }

        private void OnEnable()
        {
            _hubLevel.Trigger.OnPlayerEnter.AddListener(EnteredHub);
            _gameLevel.Trigger.OnPlayerEnter.AddListener(EnteredGameLevel);
        }

        
        private void OnDisable()
        {
            _hubLevel.Trigger.OnPlayerEnter.RemoveListener(EnteredHub);
        }
        
        private void EnteredHub()
        {
            _gameLevel.Level.UnloadScene();
        }
        
        private void EnteredGameLevel()
        {
            _hubLevel.Level.UnloadScene();

        }
    }
}