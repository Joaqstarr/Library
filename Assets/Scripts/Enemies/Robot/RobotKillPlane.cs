using UnityEngine;

namespace Enemies.Robot
{
    public class RobotKillPlane : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out RobotStateManager robot))
            {
                Destroy(robot.gameObject);
            }
        }
    }
}