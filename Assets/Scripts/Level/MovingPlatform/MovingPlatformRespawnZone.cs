using Player;
using UI;
using UnityEngine;

namespace Level.MovingPlatform
{
    public class MovingPlatformRespawnZone : MonoBehaviour
    {
        [SerializeField] private Transform _respawnPoint;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Player.PlayerMovement playerMovement = other.GetComponent<Player.PlayerMovement>();
                Player.PlayerSafePoint playerSafePoint = other.GetComponent<Player.PlayerSafePoint>();
                if (playerSafePoint != null)
                {
                    playerSafePoint.PlayFallSound();
                }
                if (ScreenFader.Instance)
                {
                    ScreenFader.Instance.Fade(0.5f, 0.5f, () =>
                    {
                        TeleportToSafety(playerMovement, _respawnPoint);
                    }, null);
                }else
                {

                    TeleportToSafety(playerMovement, _respawnPoint);
                }
            }
        }

        private static void TeleportToSafety(Player.PlayerMovement playerMovement, Transform respawnPoint)
        {

            
            playerMovement.ResetVelocity();
            playerMovement.Teleport(respawnPoint.position);
            PlayerHealth playerHealth = playerMovement.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Damage(1);
            }
        }
    }
}