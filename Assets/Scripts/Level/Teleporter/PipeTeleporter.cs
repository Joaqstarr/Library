using System;
using Player;
using UI;
using UnityEngine;

namespace Level.Teleporter
{
    public class PipeTeleporter : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _endLocation;
        
        public void Teleport(PlayerStateManager player)
        {
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
            player.PlayerMovementInstance.Teleport(_endLocation);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_endLocation, 0.5f);
        }
    }
}