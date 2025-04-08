using System;
using System.Collections.Generic;
using Systems.Steam;
using UnityEngine;

namespace Player.Attack
{
    public class Hitbox : MonoBehaviour
    {
        private SteamResourceHolder _playerHolder;
        public List<SteamResourceHolder> SteamHoldersInRange { get; private set; } = new List<SteamResourceHolder>();

        private void Awake()
        {
            _playerHolder = GetComponentInParent<SteamResourceHolder>();
        }

        private void OnTriggerEnter(Collider other)
        {
            SteamResourceHolder holder = other.GetComponent<SteamResourceHolder>();

            if (holder == _playerHolder) return;
            
            
            if (holder != null && !SteamHoldersInRange.Contains(holder))
            {
                SteamHoldersInRange.Add(holder);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            SteamResourceHolder holder = other.GetComponent<SteamResourceHolder>();
            
            if (holder != null)
            {
                SteamHoldersInRange.Remove(holder);
            }
        }
    }
}