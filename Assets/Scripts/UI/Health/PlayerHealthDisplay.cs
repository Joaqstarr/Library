using System;
using Player;
using Systems.Gamemode;
using TMPro;
using UnityEngine;

namespace UI.Health
{
    public class PlayerHealthDisplay : MonoBehaviour
    {

        [SerializeField] private HealthIcon[] _healthIcons;

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
            PlayerHealth health = player.GetComponent<PlayerHealth>();

            health.OnHealthChanged += OnHealthChanged;
            
            OnHealthChanged(health.CurrentHealth, health.CurrentHealth);
        }

        private void OnHealthChanged(int newhealth, int oldhealth)
        {
            
            for (int i = 0; i < _healthIcons.Length; i++)
            {
                if (i < newhealth)
                {
                    _healthIcons[i].Show();
                }
                else
                {
                    _healthIcons[i].Hide();
                }
            }
        }
    }
}