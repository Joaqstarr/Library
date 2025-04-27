using System;
using DG.Tweening;
using Player.Attack;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionStateDebugText : MonoBehaviour
    {
        [SerializeField] private Image _inhaleIcon;
        [SerializeField] private Image _exhaleIcon;

        [SerializeField] private Vector3 _scalePunchAmount;
        [SerializeField] private float _scalePunchDuration;

        [SerializeField] private float _shakeStrength = 1;
        private void Awake()
        {
            UpdateState(true);
        }

        public void UpdateState(bool isInhaling)
        {
            if (isInhaling)
            {
                _inhaleIcon.color = Color.white;
                _exhaleIcon.color = Color.clear;
            }
            else
            {
                _inhaleIcon.color = Color.clear;
                _exhaleIcon.color = Color.white;
            }

            transform.DOComplete();
            transform.DOShakePosition(_shakeStrength, _scalePunchDuration, 10);
            transform.DOPunchScale(_scalePunchAmount, _scalePunchDuration, 1, 0.5f)
                .OnComplete(() =>
                {
                    transform.localScale = Vector3.one;
                });
        }

        private void OnEnable()
        {
            PlayerAttackManager.OnAttackStateChange += UpdateState;
            
        }

        private void OnDisable()
        {
            PlayerAttackManager.OnAttackStateChange -= UpdateState;
        }
    }
}