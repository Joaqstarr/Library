using Player;
using Player.Attack;
using UnityEngine;

namespace Systems.Gamemode
{
    public class Gamemanager : MonoBehaviour
    {
        public static Gamemanager Instance { get; private set; }

        [SerializeField] private PlayerStateManager _playerPrefab;

        private PlayerStateManager _player;
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
            
            
        }
        
        public void TrySpawnPlayer(PlayerStart playerStart)
        {
            if (_player == null)
            {
                if (playerStart)
                {
                    _player = Instantiate(_playerPrefab, playerStart.transform.position, playerStart.transform.rotation);
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
        
    }
}