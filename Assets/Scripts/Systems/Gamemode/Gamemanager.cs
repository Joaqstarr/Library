using Player;
using Player.Attack;
using UnityEngine;
using Systems.SaveSystem;
using Utility.SceneManagement;

namespace Systems.Gamemode
{
    public class Gamemanager : MonoBehaviour
    {
        public static Gamemanager Instance { get; private set; }

        [SerializeField] private PlayerStateManager _playerPrefab;

        private PlayerStateManager _player;

        private DataSaver _dataSaver;
        private SaveData _saveData;

        [SerializeField]
        private SceneReference _defaultLevel;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Application.backgroundLoadingPriority = ThreadPriority.Low;


            _dataSaver = new DataSaver("save.boogers");

            _saveData = _dataSaver.LoadData();
        }
        
        

        public void SaveData()
        {
            _dataSaver.SaveData(_saveData);
        }

        private void Start()
        {
            // Initialize game state or other components here
            
            /*
            PlayerStart playerStart = GetPrincipalPlayerStart();

            if (playerStart)
            {
                _player = Instantiate(_playerPrefab, playerStart.transform.position, playerStart.transform.rotation);
                
            }*/


            LoadCurrentLevel();

            SaveData();
        }

        public void SetCurrentLevel(SceneReference level)
        {
            if (level != null)
            {
                _saveData.CurrentLevel = level;
            }
        }

        public void LoadCurrentLevel()
        {
            if (_saveData.CurrentLevel == null  )
            {
                _saveData.CurrentLevel = _defaultLevel;
            }

            if (_saveData.CurrentLevel)
            {
                _saveData.CurrentLevel.LoadScene();
            }
        }

        public void TrySpawnPlayer(PlayerStart playerStart)
        {
            if (_player == null)
            {
                if (playerStart)
                {
                    _player = Instantiate(_playerPrefab, playerStart.transform.position, playerStart.transform.rotation);

                    PlayerHealth health = _player.GetComponent<PlayerHealth>();

                    if (health)
                    {
                        health.OnHealthDepleted += OnDead;
                    }
                }
            }
        }

        private static PlayerStart GetPrincipalPlayerStart()
        {
            var playerStarts = FindObjectsByType<PlayerStart>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            if(playerStarts.Length>0)
            {
                var playerStart = playerStarts[0];
                
                for(int i = 0; i < playerStarts.Length; i++)
                {
                    if(playerStarts[i].Priority > playerStart.Priority)
                    {
                        playerStart = playerStarts[i];
                    }
                }
                
                return playerStart;

            }
            else
            {
                Debug.LogError("No PlayerStart found in the scene.");

                return null;
            }
        }


        public void OnDead(int currentHealth, int oldHealth)
        {
            Destroy(_player.gameObject);
            _player = null;
            LoadCurrentLevel();
        }

        public SaveData GetSaveData()
        {
            return _saveData;
        }
    }
}