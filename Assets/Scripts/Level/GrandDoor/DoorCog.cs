using System;
using UnityEngine;

namespace Level.GrandDoor
{
    public class DoorCog : MonoBehaviour
    {
        private GrandDoor _grandDoor;
        [SerializeField]
        private float _speed = 100f;

        private void Start()
        {
            _grandDoor = GetComponentInParent<GrandDoor>();
        }

        private void Update()
        {
            if (!_grandDoor.Isloading)
            {
                return;
            } 

            Vector3 localEulers = transform.localEulerAngles;
            
            localEulers.x += _speed * Time.deltaTime;

            transform.localEulerAngles = localEulers;
        }
    }
}