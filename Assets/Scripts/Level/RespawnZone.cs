using System;
using Player;
using UI;
using UnityEngine;

namespace Level
{
    public class RespawnZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Player.PlayerSafePoint playerSafePoint = other.GetComponent<Player.PlayerSafePoint>();

                if (ScreenFader.Instance)
                {
                 
                    ScreenFader.Instance.Fade(0.5f, 0.5f, () =>
                    {
                        TeleportToSafety(playerSafePoint);
                    }, null);
                }else
                {
                    TeleportToSafety(playerSafePoint);
                }
            }
        }

        private static void TeleportToSafety(PlayerSafePoint playerSafePoint)
        {
            if (playerSafePoint != null)
            {
                playerSafePoint.TeleportToSafePoint();
            }
        }
    }
}