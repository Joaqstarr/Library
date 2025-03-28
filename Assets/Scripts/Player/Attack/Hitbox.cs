using System;
using System.Collections.Generic;
using Systems.Steam;
using UnityEngine;

namespace Player.Attack
{
    public class Hitbox : MonoBehaviour
    {
        public List<SteamResourceHolder> SteamHoldersInRange { get; private set; } = new List<SteamResourceHolder>();

        private void OnTriggerEnter(Collider other)
        {
            SteamResourceHolder holder = other.GetComponent<SteamResourceHolder>();
            if (holder != null)
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