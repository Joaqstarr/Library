using System;
using Cinemachine;
using UnityEngine;

namespace Level.Camera
{
    public class CameraVolume : MonoBehaviour
    {
        [SerializeField] private int _priority = 11;
        
        //if true sets look and follow target to player on entering collision, and sets to null when exits
        [SerializeField]private bool _setLookTarget = true;
        [SerializeField]private bool _setFollowTarget = true;

        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            if (!_virtualCamera)
            {
                _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            }

            _virtualCamera.Priority = -1;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _virtualCamera.Priority = 11;
                if (_setLookTarget)
                {
                    _virtualCamera.LookAt = other.transform;
                }

                if (_setFollowTarget)
                {
                    _virtualCamera.Follow = other.transform;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _virtualCamera.Priority = -1;
                
                if (_setLookTarget)
                {
                    _virtualCamera.LookAt = null;
                }

                if (_setFollowTarget)
                {
                    _virtualCamera.Follow = null;
                }
            }
        }
    }
}