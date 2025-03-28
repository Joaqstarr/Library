using System;
using Cinemachine;
using UnityEngine;

namespace Player.Attack
{
    public class HitboxRotator : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCameraBase _thirdPersonCamera;


        private void Update()
        {

            if (CinemachineCore.Instance.IsLive(_thirdPersonCamera))
            {
                transform.forward = Camera.main.transform.forward;
            }
            else
            {
                transform.forward = transform.parent.forward;
            }
        }
    }
}