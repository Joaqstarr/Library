﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.MovingPlatform
{
    [RequireComponent(typeof(MovingPlatformVelocityTransfer))]
    public class MovingPlatformBehavior : MonoBehaviour
    {
        public Vector3[] localWaypoints;
        public float speed = 2f;

#if UNITY_EDITOR
        [HideInInspector] public bool editMode = false; 
#endif

        protected int targetIndex = 0;
        private bool forward = true;

        protected Vector3 CurrentTarget => transform.parent.TransformPoint(localWaypoints[targetIndex]);

        

        [SerializeField]
        private bool _playOnStart = true;

        protected bool _isPlaying = false;
        
        
        [SerializeField]
        private bool _shouldLoop = true;

        protected bool _hasStarted = false;
        private void Start()
        {
            _isPlaying = _playOnStart;
        }

        protected virtual void Update()
        {

            if (localWaypoints.Length == 0 || !_isPlaying) return;

            Vector3 target = CurrentTarget;
            

            
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                if(targetIndex == 0 || targetIndex == localWaypoints.Length - 1)
                {
                    if (!_shouldLoop && _hasStarted)
                    {
                        _isPlaying = false;
                        return;
                    }
                }
                
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

                _hasStarted = true;
            }
        }

        public virtual void StartMovement()
        {
            _isPlaying = true;
            _hasStarted = false;
        }
        

        
        public void StopMovement()
        {
            _isPlaying = false;
        }
    }
}