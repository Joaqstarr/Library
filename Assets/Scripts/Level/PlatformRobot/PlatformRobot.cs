using System;
using UnityEngine;
using UnityEngine.Splines;

namespace Level.PlatformRobot
{
    public class PlatformRobot : MonoBehaviour
    {
        private SplineAnimate _splineAnimateComponent;
        
        private void Awake()
        {
            _splineAnimateComponent = GetComponent<SplineAnimate>();
        }

        private void Start()
        {
            
            //Invoke(nameof(StartMovement), 3f);
        }

        public void StartMovement()
        {
            _splineAnimateComponent.Play();
        }

        public void StopMovement()
        {
            _splineAnimateComponent.Pause();

        }
    }
}