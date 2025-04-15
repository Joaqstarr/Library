using System;
using Systems.Gamemode;
using UnityEngine;

namespace Player
{
    public class PlayerStart : MonoBehaviour
    {
        [field: SerializeField] public int Priority { get; private set; } = 0;
        [field: SerializeField] private PlayerStateManager _playerPrefab;

        private void Start()
        {
            Debug.Log(Gamemanager.Instance);
            if(Gamemanager.Instance == null)
            {
                var player = Instantiate(_playerPrefab, transform.position, transform.rotation);
                player.transform.parent = transform.parent;
            }
            else
            {
                Gamemanager.Instance.TrySpawnPlayer(this);
            }
        }
    }
}