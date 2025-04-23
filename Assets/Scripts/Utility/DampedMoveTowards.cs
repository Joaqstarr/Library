using System;
using UnityEngine;

namespace Utility
{
    public class DampedMoveTowards : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothTime = 0.3f;

        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            transform.position = _target.position;
        }

        private void Update()
        {
            if (_target != null)
            {
                // Smoothly move towards the target position
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    _target.position,
                    ref _velocity,
                    _smoothTime
                );
            }
        }
    }
}