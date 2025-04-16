using System;
using Systems.Gamemode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public delegate void OnHealthChangedSignature(int newHealth, int oldHealth);
        public OnHealthChangedSignature OnHealthChanged;
        public OnHealthChangedSignature OnHealthDepleted;
        
        [SerializeField]
        private int _health = 3;
        
        [SerializeField]
        private float _invincibilityTime = 1f;
        private float _invincibilityTimer = 0f;
        
        public void Damage(int damage)
        {
            if(_invincibilityTimer > 0 || _health <= 0)
                return;
            
            _invincibilityTimer = _invincibilityTime;
            
            int oldHealth = _health;
            _health -= damage;
            
            OnHealthChanged?.Invoke(_health, oldHealth);
            if (_health <= 0)
            {
                
                OnHealthDepleted?.Invoke(_health, oldHealth);
                
                Respawn();
            }
        }

        private void Update()
        {
            _invincibilityTimer -= Time.deltaTime;
        }

        private void Respawn()
        {
            // Handle player death
            
            //reload scene if game manager is not present
            if (!Gamemanager.Instance)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}