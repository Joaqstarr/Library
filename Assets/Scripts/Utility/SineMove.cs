using System;
using UnityEngine;

namespace Utility
{
    public class SineMove : MonoBehaviour
    {
        [SerializeField] private Vector3 _scale = Vector3.zero;

        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private float _frequency = 1f;
        private Vector3 _startPos;

        private void Awake()
        {
            _startPos = transform.localPosition;
        }

        private void Update()
        {
            float sineAmount = Mathf.Sin(Time.time * _frequency) * _amplitude;
            Vector3 offset = sineAmount * _scale;

            transform.localPosition = _startPos + offset;
        }
    }
}