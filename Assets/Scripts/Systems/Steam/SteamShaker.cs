using System;
using UnityEngine;

namespace Systems.Steam
{
    public class SteamShaker : MonoBehaviour
    {
        [SerializeField] private SteamResourceHolder _steamResourceHolder;
        [SerializeField] private float _wiggleFrequency = 2f;
        [SerializeField] private float _wiggleAmplitude = 0.1f;

        private Vector3 _originalScale;

        private void Start()
        {
            _originalScale = transform.localScale;
        }

        private void Update()
        {
            if (_steamResourceHolder == null) return;

            float strength = _steamResourceHolder.SteamFillPercent;
            float wiggle = Mathf.Sin(Time.time * _wiggleFrequency) * _wiggleAmplitude * strength;

            transform.localScale = _originalScale + Vector3.one * wiggle;
        }
    }
}