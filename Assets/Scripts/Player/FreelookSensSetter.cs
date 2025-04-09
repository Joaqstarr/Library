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
            if (_cam != null)
            {
                _cam.m_XAxis.m_MaxSpeed = Sens * 100;
                _cam.m_YAxis.m_MaxSpeed = Sens * 2;
            }
            else
            {
                Debug.LogError("CinemachineFreeLook component not found on the object.");
            }
        }
    }
}