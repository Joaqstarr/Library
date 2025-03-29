using System;
using DG.Tweening;
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

        private void Awake()
        {
            _startingParent = transform.parent;
        }

        public void StartStompRing(Vector3 position)
        {
            
            transform.position = position;
            transform.localScale = _startSize;//new Vector3(0.01f, 1f, 0.01f);
            gameObject.SetActive(true);
            
            
        }

        private void OnEnable()
        {
            transform.parent = null;
            
            _dealingDamage = true;
            transform.localScale = _startSize; //new Vector3(0.01f, 1f, 0.01f);
            transform.DOScale(_endSize, _growTime).SetEase(_growEase).onComplete += () =>
            {
                _dealingDamage = false;
                Vector3 fizzleSize = _endSize;
                fizzleSize.y = 0.01f;
                transform.DOScale(fizzleSize, 0.5f).onComplete += () =>
                {
                    gameObject.SetActive(false);
                };
            };
        }

        private void OnDisable()
        {
            transform.localScale = new Vector3(0.01f, 1f, 0.01f);
            transform.parent = _startingParent;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_dealingDamage) return;
            
            Debug.Log(other.gameObject.name +" Collision! Deal Damage Here.");
        }
    }
}