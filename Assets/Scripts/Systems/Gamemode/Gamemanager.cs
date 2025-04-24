using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Player;
using Player.Attack;
using UnityEngine;
using Systems.SaveSystem;
using UnityEngine.SceneManagement;
using Utility.SceneManagement;

namespace Systems.Gamemode
{
    public class Gamemanager : MonoBehaviour
    {
        public static Gamemanager Instance { get; private set; }

        public delegate void LevelLoadedFromSaveSignature(SceneReference scene);
        public static LevelLoadedFromSaveSignature OnLevelLoadedFromSave;

        public delegate void OnPlayerSpawnedSignature(PlayerStateManager player);
        public static OnPlayerSpawnedSignature OnPlayerSpawned;
        
        [SerializeField] private PlayerStateManager _playerPrefab;

        private PlayerStateManager _player;

        private DataSaver _dataSaver;
        private SaveData _saveData;

        [SerializeField]
        private SceneReference _defaultLevel;
        
        [SerializeField]
        private Camera _mainCamera;

        private CinemachineBrain _cinemachineBrain;
        
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

            _dataSaver = new DataSaver("save.boogers");

            _cinemachineBrain = _mainCamera.GetComponent<CinemachineBrain>();
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

        public void LoadCurrentLevel(bool forceReload = false)
        {
            if (_saveData.CurrentLevel == null  )
            {
                SetCurrentLevel(_defaultLevel);
            }

            if (_saveData.CurrentLevel)
            {
                _saveData.CurrentLevel.LoadScene(forceReload);
            }
        }

        public void TrySpawnPlayer(PlayerStart playerStart)
        {
            if (_player == null)
            {
                if (playerStart)
                {
                    float defaultBlendTime = _cinemachineBrain.m_DefaultBlend.m_Time;
                    _cinemachineBrain.m_DefaultBlend.m_Time = 0f;
                    _player = Instantiate(_playerPrefab, playerStart.transform.position, playerStart.transform.rotation);

                    PlayerHealth health = _player.GetComponent<PlayerHealth>();

                    if (health)
                    {
                        health.OnHealthDepleted += OnDead;
                    }
                    

                    OnPlayerSpawned?.Invoke(_player);

                    StartCoroutine(ResetBlend());
                    IEnumerator ResetBlend()
                    {
                        yield return new WaitForSeconds(0.1f);
                        _cinemachineBrain.m_DefaultBlend.m_Time = defaultBlendTime;

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
            LoadCurrentLevel(true);
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