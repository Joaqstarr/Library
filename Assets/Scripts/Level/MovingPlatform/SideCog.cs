using System;
using UnityEngine;

namespace Level.MovingPlatform
{
    public class SideCog : MonoBehaviour
    {
        private MovingPlatformVelocityTransfer _movingPlatformBehavior;

        [SerializeField] private float _rotateSpeed = 40f;
        private void Awake()
        {
            _movingPlatformBehavior = GetComponentInParent<MovingPlatformVelocityTransfer>();
            
            
        }

        private void Update()
        {
            float vel = _movingPlatformBehavior.CurrentDelta.magnitude / Time.deltaTime;
            
            

            Vector3 euler = transform.localEulerAngles;

            euler.y += _rotateSpeed * Time.deltaTime * vel;
            transform.localEulerAngles = euler;
        }
    }
}