using System;
using Player;
using UI;
using UnityEngine;

namespace Level.Teleporter
{
    public class PipeTeleporter : MonoBehaviour
    {
        [SerializeField]
        private Transform _endLocation;
        
        public void Teleport(PlayerStateManager player)
        {
            if (!_endLocation)
            {
                Debug.LogError("No location set in pipe teleporter", gameObject);
                return;
            }
            player.SwitchToCinematicState();

            if (ScreenFader.Instance)
            {
                ScreenFader.Instance.Fade(1, 2, () => 
                {
                    TeleportToEndLocation(player);
                }, () =>
                {
                    player.SwitchToLocomotionState();

                });
            }
        }

        private void TeleportToEndLocation(PlayerStateManager player)
        {
            player.SwitchToCinematicState();
            player.PlayerMovementInstance.Teleport(_endLocation.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            if (_endLocation)
            {
                Gizmos.DrawSphere(_endLocation.position, 0.5f);

            }
        }
    }
}