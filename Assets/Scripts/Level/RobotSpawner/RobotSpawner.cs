using System;
using Enemies.Robot;
using UnityEngine;

namespace Level.RobotSpawner
{
    public class RobotSpawner : MonoBehaviour
    {
        [SerializeField] private RobotStateManager _robotPrefab;


        private void Start()
        {
            InvokeRepeating(nameof(SpawnRobot), 2, 5f);
        }

        public void SpawnRobot()
        {
            RobotStateManager robot = Instantiate(_robotPrefab, transform.position, Quaternion.identity);
            
            robot.transform.SetParent(transform.parent);
            
            robot.PlaySpawnAnimation();
        }
    }
}