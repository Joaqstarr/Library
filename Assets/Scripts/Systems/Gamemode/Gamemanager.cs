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

        public delegate void LevelLoadedFromSaveSignature(SceneReference scene);
        public static LevelLoadedFromSaveSignature OnLevelLoadedFromSave;
        
        
        [SerializeField] private PlayerStateManager _playerPrefab;

        private PlayerStateManager _player;

        private DataSaver _dataSaver;
        private SaveData _saveData;

        [SerializeField]
        private SceneReference _defaultLevel;
        
        [SerializeField]
        private Camera _mainCamera;

        [SerializeField] private bool _shouldSave = true;
        private void Awake()
        {
            if (!Application.isEditor) _shouldSave = true;
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Application.backgroundLoadingPriority = ThreadPriority.Low;
            _mainCamera.gameObject.SetActive(false);

            _dataSaver = new DataSaver("save.boogers");

        }

        private void LoadData()
        {
            _saveData = _dataSaver.LoadData();
            LoadCurrentLevel();

            OnLevelLoadedFromSave?.Invoke(_saveData.CurrentLevel);

        }


        public void SaveData()
        {
            if(_shouldSave)
                _dataSaver.SaveData(_saveData);
        }

        private void Start()
        {
            LoadData();
        }

        public void SetCurrentLevel(SceneReference level)
        {
            if (level != null)
            {
                _saveData.CurrentLevel = level;
                SaveData();
            }
        }

        public void LoadCurrentLevel()
        {
            if (_saveData.CurrentLevel == null  )
            {
                SetCurrentLevel(_defaultLevel);
            }

            if (_saveData.CurrentLevel)
            {
                _saveData.CurrentLevel.LoadScene(false);
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
                    
                    _mainCamera.gameObject.SetActive(true);

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

        public PlayerStateManager GetPlayer()
        {
            return _player;
        }
    }
}