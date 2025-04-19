using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Utility.ScreenShake
{
    public class ScreenShaker : MonoBehaviour
    {
        private bool _isShaking = false;

        private CinemachineImpulseSource _impulse;

        private void Awake()
        {
            _impulse = GetComponent<CinemachineImpulseSource>();
        }

        public void StartShaking()
        {
            StartCoroutine(StartLoopingScreenshake(_impulse.m_ImpulseDefinition.m_TimeEnvelope.Duration));
        }

        public void StopShaking()
        {
            _isShaking = false;
        }

        IEnumerator StartLoopingScreenshake(float length)
        {
            _isShaking = true;
            while (_isShaking)
            {
                _impulse.GenerateImpulseWithForce(1);
                yield return new WaitForSeconds(length*0.9f);
            }
            
        }
    }
}