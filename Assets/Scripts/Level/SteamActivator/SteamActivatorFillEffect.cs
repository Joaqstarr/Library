using System;
using Systems.Steam;
using UnityEngine;

namespace Level.SteamActivator
{
    public class SteamActivatorFillEffect : MonoBehaviour
    {
        [SerializeField]
        private SteamResourceHolder _resourceHolder;
        
        float _scaleSpeed = 5f;

        private void Update()
        {
            if(!_resourceHolder) return;
            
            float targetYScale = _resourceHolder.SteamFillPercent;
            
            Vector3 newScale = transform.localScale;
            newScale.y = Mathf.Lerp(newScale.y, targetYScale, Time.deltaTime * _scaleSpeed);
            
            transform.localScale = newScale;
        }
    }
}