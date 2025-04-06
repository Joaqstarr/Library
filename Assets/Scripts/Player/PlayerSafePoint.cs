using System;
using UnityEngine;

namespace Player
{
    public class PlayerSafePoint : MonoBehaviour
    {
        private Vector3 _safePoint;
        private PlayerMovement _playerMovement;
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _safePoint = transform.position;
        }


        private void Update()
        {
            if (_playerMovement.IsGrounded() && _playerMovement.GroundedRaycast(out RaycastHit hit))
            {
                if(hit.transform && hit.transform.gameObject.isStatic)
                {
                    _safePoint = transform.position;
                }
            }
        }
        
        public void TeleportToSafePoint()
        {
            _playerMovement.ResetVelocity();
            _playerMovement.Teleport(_safePoint);
        }
    }
}