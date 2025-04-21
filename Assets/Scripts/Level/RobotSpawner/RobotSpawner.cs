using System;
using System.Collections.Generic;
using Enemies.Robot;
using UnityEngine;

namespace Level.RobotSpawner
{
    public class RobotSpawner : MonoBehaviour
    {
        [SerializeField] private RobotStateManager _robotPrefab;

        [SerializeField] private float _spawnDelay = 5f;

        private Transform[] _spawnPoints;
        
        private List<RobotStateManager> _robots = new List<RobotStateManager>();

        [SerializeField] private int _maxEnemies = 5;
        private void Awake()
        {
            _spawnPoints = GetComponentsInChildren<Transform>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnRobot), 1, _spawnDelay);
        }
        

        public void SpawnRobot()
        {
            for (int i = _robots.Count - 1; i >= 0; i--)
            {
                if (_robots[i] == null)
                {
                    _robots.RemoveAt(i);
                }
            }
            
            if(_robots.Count >= _maxEnemies) return;
            
            Transform spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
            
            RobotStateManager robot = Instantiate(_robotPrefab, spawnPoint.position, Quaternion.identity, transform.parent);
            _robots.Add(robot);
            robot.PlaySpawnAnimation();
        }
    }
}