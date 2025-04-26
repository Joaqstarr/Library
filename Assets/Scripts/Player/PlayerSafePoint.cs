using System;
using Audio;
using UnityEngine;

namespace Player
{
    public class PlayerSafePoint : MonoBehaviour
    {
        private Vector3 _safePoint;
        private PlayerMovement _playerMovement;
        [SerializeField] private RandomClipPlayer _fallSound;
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _safePoint = transform.position;
        }


        private void Update()
        {
            if (_playerMovement.IsGrounded() && _playerMovement.GroundedRaycast(out RaycastHit hit))
            {
                if(hit.transform && hit.transform.CompareTag("SafeZone"))
                {
                    _safePoint = transform.position;
                }
            }
        }

        public void PlayFallSound()
        {
            _fallSound?.PlayRanClip();
        }
        public void TeleportToSafePoint()
        {
            _playerMovement.ResetVelocity();
            _playerMovement.Teleport(_safePoint);
        }
    }
}