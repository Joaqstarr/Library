using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.MovingPlatform
{
    public class MovingPlatformBehavior : MonoBehaviour
    {
        public Vector3[] localWaypoints;
        public float speed = 2f;

#if UNITY_EDITOR
        [HideInInspector] public bool editMode = false; 
#endif

        private int targetIndex = 0;
        private bool forward = true;

        private Vector3 CurrentTarget => transform.parent.TransformPoint(localWaypoints[targetIndex]);

        
        private Vector3 _previousPosition;

        [SerializeField]
        private bool _playOnStart = true;
        public Vector3 CurrentDelta { get; private set; }

        private bool _isPlaying = false;
        private void Start()
        {
            _previousPosition = transform.position;


            _isPlaying = _playOnStart;
        }

        void Update()
        {

            if (localWaypoints.Length == 0 || !_isPlaying) return;

            Vector3 target = CurrentTarget;
            
            CurrentDelta = transform.position - _previousPosition;
            _previousPosition = transform.position;
            
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                if (forward)
                {
                    targetIndex++;
                    if (targetIndex >= localWaypoints.Length)
                    {
                        targetIndex = localWaypoints.Length - 2;
                        forward = false;
                    }
                }
                else
                {
                    targetIndex--;
                    if (targetIndex < 0)
                    {
                        targetIndex = 1;
                        forward = true;
                    }
                }
            }
        }

        public void StartMovement()
        {
            _isPlaying = true;
        }
        
        public void StopMovement()
        {
            _isPlaying = false;
        }
    }
}