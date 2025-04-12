using System;
using Cinemachine;
using UnityEngine;

namespace Player
{
    public class FreelookSensSetter : MonoBehaviour
    {
        private CinemachineFreeLook _cam;

        public static float Sens = 1;
        private void Awake()
        {
            _cam = GetComponent<CinemachineFreeLook>();
        }

        private void Update()
        {
            if (_cam == null) return;
            
            
            if (Time.deltaTime == 0)
            {
                _cam.enabled = false;
            }
            else
            {
                if (_cam.enabled == false)
                {
                    _cam.enabled = true;
                }
                _cam.m_XAxis.m_MaxSpeed = Sens * 2;
                _cam.m_YAxis.m_MaxSpeed = Sens * 0.02f;
            }
        }
    }
}