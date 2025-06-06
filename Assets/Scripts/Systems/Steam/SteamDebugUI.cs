﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Steam
{
    public class SteamDebugUI : MonoBehaviour
    {
        [SerializeField]
        private SteamResourceHolder _holder;
        private TMP_Text _text;
        [SerializeField]
        private Image _bgMeter;

        [SerializeField] private bool _shouldFaceCamera = true;
        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        public void SetSteamResourceHolder(SteamResourceHolder holder)
        {
            _holder = holder;
        }

        private void Update()
        {
            if(_shouldFaceCamera)
                transform.forward =  transform.position - Camera.main.transform.position;
            
            if(_holder == null)
                return;
            
            _text.text = $"{_holder.SteamAmount:0.0} / {_holder.MaxSteamAmount:0.0}";

            _bgMeter.fillAmount = _holder.SteamFillPercent;
        }
        
        
    }
}