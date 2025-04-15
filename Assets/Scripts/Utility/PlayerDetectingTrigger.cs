using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    public class PlayerDetectingTrigger : MonoBehaviour
    {

        public UnityEvent OnPlayerEnter;
        public UnityEvent OnPlayerExit;


        private GameObject _player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEnter?.Invoke();
                _player = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_player == other.gameObject)
            {
                OnPlayerExit?.Invoke();
                _player = null;
            }
        }
    }
}