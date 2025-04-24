using System;
using Player;
using Systems.Gamemode;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerHealthDisplay : MonoBehaviour
    {

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
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
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            _text.text = health.CurrentHealth.ToString();

            health.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int newhealth, int oldhealth)
        {
            _text.text = newhealth.ToString();
        }
    }
}