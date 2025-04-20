using System;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Enemies.Robot.Attacks
{
    public class StompRing : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _startSize = new Vector3(0.2f, 1f, 0.2f);

        [SerializeField]
        private Vector3 _endSize = new Vector3(1f, 1f, 1f);

        [SerializeField]
        private float _growTime = 1;
        [SerializeField]
        private Ease _growEase = Ease.InCubic;

        private bool _dealingDamage = false;

        private Transform _startingParent = null;
        
        private Tweener _tweener = null;

        private void Awake()
        {
        }

        public void StartStompRing(Vector3 position)
        {
            
            transform.position = position;
            transform.localScale = _startSize;//new Vector3(0.01f, 1f, 0.01f);
            
            if(_startingParent == null)
                _startingParent = transform.parent;

            transform.parent = _startingParent.parent;
            gameObject.SetActive(true);
            
            
        }

        private void OnEnable()
        {
            
            _dealingDamage = true;
            transform.localScale = _startSize; //new Vector3(0.01f, 1f, 0.01f);
            _tweener = transform.DOScale(_endSize, _growTime).SetEase(_growEase);
            _tweener.onComplete += () =>
            {
                _dealingDamage = false;
                Vector3 fizzleSize = _endSize;
                fizzleSize.y = 0.01f;
                transform.DOScale(fizzleSize, 0.5f).onComplete += () =>
                {
                    DisableRing();
                };
            };
        }

        private void DisableRing()
        {
            if (_startingParent == null)
            {
                Destroy(gameObject);
            }
            transform.parent = _startingParent;
            gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            transform.localScale = new Vector3(0.01f, 1f, 0.01f);
            if (_tweener != null && _tweener.IsActive())
            {
                _tweener.Kill();
            }

            _tweener = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_dealingDamage) return;

            if (other.CompareTag("Player"))
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();

                if (health)
                {
                    health.Damage(1);
                }
            }
        }
    }
}