using System;
using Cinemachine;
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
        private int _maxHealth = 3;

        private int _health;
        [SerializeField]
        private float _invincibilityTime = 1f;
        private float _invincibilityTimer = 0f;

        [SerializeField] private float _postDamageHealTimer = 15;
        [SerializeField] private float _healTime = 5;
        private float _healTimer = 0f;


        private CinemachineImpulseSource _cinemachineImpulseSource;

        private void Awake()
        {
            _health = _maxHealth;
            _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void Damage(int damage)
        {
            if(_invincibilityTimer > 0 || _health <= 0)
                return;
            
            _invincibilityTimer = _invincibilityTime;
            
            int oldHealth = _health;
            _health -= damage;
            _cinemachineImpulseSource.GenerateImpulseWithForce(1);
            OnHealthChanged?.Invoke(_health, oldHealth);
            _healTimer = _postDamageHealTimer;
            if (_health <= 0)
            {
                
                OnHealthDepleted?.Invoke(_health, oldHealth);
                
                Respawn();
            }
        }

        public void Heal(int amt)
        {
            int oldHealth = _health;
            _health += amt;
            
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            
            if(_health != oldHealth)
                OnHealthChanged?.Invoke(_health, oldHealth);        
        }

        private void Update()
        {
            _invincibilityTimer -= Time.deltaTime;

            if (_health != _maxHealth)
            {
                _healTimer -= Time.deltaTime;
            }
            else
            {
                _healTimer = _healTime;
            }
            
            
            if (_healTimer <= 0)
            {
                Heal(1);
                _healTimer = _healTime;
            }
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