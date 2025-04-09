using System;
using Systems.Steam;
using UnityEngine;
using UnityEngine.VFX;

namespace Level.HotAirBalloon
{
    public class HotAirBalloon : MonoBehaviour
    {
        private SteamResourceHolder _resourceHolder;
        [SerializeField] private float _speed = 5f;
        [SerializeField]
        private float _minHeightLocal = -1f;
        [SerializeField]
        private float _maxHeightLocal = 1f;

        private float _minWorldHeight = 0;
        private float _maxWorldHeight = 0;

        
        private float _targHeight = 0;

        private VisualEffect _flame;
        private void Awake()
        {
            _flame = GetComponentInChildren<VisualEffect>();
            _resourceHolder = GetComponentInChildren<SteamResourceHolder>();
            
            _minWorldHeight = transform.position.y + _minHeightLocal;
            _maxWorldHeight = transform.position.y + _maxHeightLocal;

            _targHeight = WorldToInterp(transform.position.y);
        }

        private void Start()
        {
            _resourceHolder.SetSteamAmount(WorldToInterp(transform.position.y) * _resourceHolder.MaxSteamAmount);
        }

        private void Update()
        {
            _targHeight = _resourceHolder.SteamFillPercent;

//            _flame.SetFloat("flameDistance", Mathf.Lerp(0, -1.29f, _targHeight));

            Vector3 targPos = transform.position;
            targPos.y = Mathf.Lerp(_minWorldHeight, _maxWorldHeight, _targHeight);
            transform.position = Vector3.MoveTowards(transform.position, targPos, Time.deltaTime * _speed);
        }

        private float WorldToInterp(float height)
        {
            return (height - _minWorldHeight) / (_maxWorldHeight - _minWorldHeight);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            if (!Application.isPlaying && Application.isEditor)
            {
                
                Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + _minHeightLocal, transform.position.z), new Vector3(transform.position.x, transform.position.y + _maxHeightLocal, transform.position.z));
            }
            else
            {

                Gizmos.DrawLine(new Vector3(transform.position.x, _minWorldHeight, transform.position.z), new Vector3(transform.position.x, _maxWorldHeight, transform.position.z));

                
            }
        }
    }
}