using System;
using UnityEngine;

namespace Level.MovingPlatform
{
    public class MovingPlatformVelocityTransfer : MonoBehaviour
    {
        private Vector3 _previousPosition;
        public Vector3 CurrentDelta { get; private set; }

        private void Start()
        {
            _previousPosition = transform.position;
        }


        private void Update()
        {
            CurrentDelta = transform.position - _previousPosition;
            _previousPosition = transform.position;
        }
    }
}