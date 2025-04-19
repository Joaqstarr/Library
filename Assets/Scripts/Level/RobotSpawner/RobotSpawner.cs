using System;
using Enemies.Robot;
using UnityEngine;

namespace Level.RobotSpawner
{
    public class RobotSpawner : MonoBehaviour
    {
        [SerializeField] private RobotStateManager _robotPrefab;

        [SerializeField] private float _spawnDelay = 5f;

        private Transform[] _spawnPoints;

        private void Awake()
        {
            _spawnPoints = GetComponentsInChildren<Transform>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnRobot), 8, _spawnDelay);
        }

        public void SpawnRobot()
        {
            Transform spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
            
            RobotStateManager robot = Instantiate(_robotPrefab, spawnPoint.position, Quaternion.identity, transform.parent);
            
            robot.PlaySpawnAnimation();
        }
    }
}