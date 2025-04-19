using System;
using Player;
using Systems.Gamemode;
using Systems.Steam;
using UnityEngine;

namespace UI
{
    public class PlayerHudSetter : MonoBehaviour
    {
        private SteamDebugUI _steamDebugUI;

        private void Awake()
        {
            _steamDebugUI = GetComponent<SteamDebugUI>();
        }

        private void OnEnable()
        {
            Gamemanager.OnPlayerSpawned += OnPlayerSpawned;
        }
        
        private void OnDisable()
        {
            Gamemanager.OnPlayerSpawned -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned(PlayerStateManager player)
        {
            SteamResourceHolder holder = player.GetComponent<SteamResourceHolder>();

            if (holder && _steamDebugUI)
            {
                _steamDebugUI.SetSteamResourceHolder(holder);
            }
        }
    }
}