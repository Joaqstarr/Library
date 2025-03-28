using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Steam
{
    public class SteamDebugUI : MonoBehaviour
    {
        private SteamResourceHolder _holder;
        private TMP_Text _text;
        private Image _bgMeter;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _bgMeter = GetComponentInChildren<Image>();
        }

        public void SetSteamResourceHolder(SteamResourceHolder holder)
        {
            _holder = holder;
        }

        private void Update()
        {
            if(_holder == null)
                return;
            
            _text.text = $"{_holder.SteamAmount:0.0} / {_holder.MaxSteamAmount:0.0}";

            _bgMeter.rectTransform.localScale = new Vector3(1, _holder.SteamFillPercent, 1);
        }
    }
}