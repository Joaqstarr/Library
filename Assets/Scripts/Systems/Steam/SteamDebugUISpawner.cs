using System;
using UnityEngine;

namespace Systems.Steam
{
    public class SteamDebugUISpawner : MonoBehaviour
    {
        [SerializeField] private SteamDebugUI _uiPrefab;
        [SerializeField] private float _verticalOffset = 3;
        private void Awake()
        {
            foreach (SteamResourceHolder o in FindObjectsByType<SteamResourceHolder>(FindObjectsInactive.Exclude,
                         FindObjectsSortMode.None))
            {
                SteamDebugUI _spawnedUI = GameObject.Instantiate(_uiPrefab, o.transform);
                _spawnedUI.transform.localPosition = Vector3.up * _verticalOffset;
                _spawnedUI.SetSteamResourceHolder(o);
            }
        }
    }
}