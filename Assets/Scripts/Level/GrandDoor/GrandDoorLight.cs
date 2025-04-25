using System;
using UnityEngine;
using Utility.SceneManagement;

namespace Level.GrandDoor
{
    public class GrandDoorLight : MonoBehaviour
    {
        [Serializable]
        private struct DoorLightInfo
        {
            public SceneReference _level;
            public Color _color;
        }
        
        [SerializeField] private DoorLightInfo[] _doorLightInfos;

        [SerializeField]
        private GrandDoor _grandDoor;

        private Light _light;
        private float _lightChangeSpeed = 2f;

        private void Awake()
        {
            _light = GetComponent<Light>();
        }

        private void Update()
        {
            foreach (var doorLightInfo in _doorLightInfos)
            {
                if (doorLightInfo._level == _grandDoor.CurrentGameLevel())
                {
                    _light.color = Color.Lerp(_light.color,  doorLightInfo._color, Time.deltaTime * _lightChangeSpeed);
                }
            }
        }
    }
}