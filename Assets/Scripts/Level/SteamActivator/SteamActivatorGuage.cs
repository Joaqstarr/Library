using System;
using Systems.Steam;
using UnityEngine;

namespace Level.SteamActivator
{
    public class SteamActivatorGuage : MonoBehaviour
    {
        private static readonly int EmissiveIntensity = Shader.PropertyToID("_EmissiveIntensity");
        private static readonly int EmissiveColorLDR = Shader.PropertyToID("_EmissiveColorLDR");

        [SerializeField]
        private SteamResourceHolder _resourceHolder;

        [SerializeField] private Vector2 _minMaxRotation = new Vector2(30, -30);

        [SerializeField] private float _rotSpeed = 5f;
        
        [SerializeField] private float _littleNeedleAmplitude = 5f;
        [SerializeField] private float _littleNeedleFrequency= 5f;
        
        private Transform _needleArtTransform;
        private Renderer _needleArtRenderer;

        private void Awake()
        {
            _needleArtTransform = transform.GetChild(0);
            _needleArtRenderer = _needleArtTransform.GetComponent<Renderer>();
        }

        private void Update()
        {
            if(!_resourceHolder) return;
            
            float targetYRot = Mathf.Lerp(_minMaxRotation.x, _minMaxRotation.y, _resourceHolder.SteamFillPercent);
            
            
            Vector3 newRot = transform.localEulerAngles;
            
            if(newRot.y > 180)
                newRot.y -= 360;
            
            newRot.y = Mathf.Lerp(newRot.y, targetYRot, Time.deltaTime * _rotSpeed);
            transform.localEulerAngles = newRot;


            if (_resourceHolder.SteamFillPercent >= 1)
            {

                Vector3 euler = Vector3.zero;
                
                euler.y = Mathf.Sin(Time.time * _littleNeedleFrequency) * _littleNeedleAmplitude;
                _needleArtTransform.localEulerAngles = euler;
                
                _needleArtRenderer.material.SetFloat(EmissiveIntensity, 5000000);
                _needleArtRenderer.material.SetColor(EmissiveColorLDR, Color.red);

            }
            else
            {
                _needleArtTransform.localEulerAngles = Vector3.zero;
                
                _needleArtRenderer.material.SetFloat(EmissiveIntensity, 100000);
                _needleArtRenderer.material.SetColor(EmissiveColorLDR, Color.red);

            }

            if (_resourceHolder.SteamFillPercent < 0.01)
            {
                _needleArtRenderer.material.SetFloat(EmissiveIntensity, 0);
                _needleArtRenderer.material.SetColor(EmissiveColorLDR, Color.black);
            }

        }
    }
}