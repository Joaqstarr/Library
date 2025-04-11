using System;
using Player.Attack;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ActionStateDebugText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void UpdateState(bool isInhaling)
        {
            _text.text = isInhaling ? "Inhaling" : "Exhaling";
            
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