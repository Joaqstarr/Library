using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

namespace Level.Pipe
{
    [RequireComponent(typeof(SplineContainer))]
    public class PipeLump : MonoBehaviour
    {
        [SerializeField, Range(0,1)] 
        private float _lumpPosition = 0;
        
        [field: SerializeField, Range(0,1)]
        public float LumpStrength { get; private set; }
        
        public Vector3 LumpPosition { get; private set; }
        private SplineContainer _splineContainer;

        private float _distanceFromEdgeToClamp;
        [SerializeField]
        private float _edgeLumpBuffer = 0.5f;
        
        private void Awake()
        {
            _splineContainer = GetComponent<SplineContainer>();
            SplineUtility.GetPointAtLinearDistance(_splineContainer.Spline, 0, _edgeLumpBuffer, out _distanceFromEdgeToClamp);

        }

        private void OnEnable()
        {
            /*
            DOVirtual.Float(0, 1, 0.3f, value =>
            {
                LumpStrength = value;
            });*/
        }
        

        private void OnDisable()
        {
            /*
            DOVirtual.Float(1, 0, 0.3f, value =>
            {
                LumpStrength = value;
            });*/
        }

        private void Update()
        {
            Vector3 pos = _splineContainer.Spline.EvaluatePosition(_lumpPosition);

            LumpPosition = pos;
            
            
        }

        private void OnDrawGizmosSelected()
        {
            if (_splineContainer == null)
            {
                _splineContainer = GetComponent<SplineContainer>();

            }
            
            
            Vector3 pos = _splineContainer.Spline.EvaluatePosition(_lumpPosition);
            pos = transform.TransformPoint(pos);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 0.2f);
        }

        public void SetLumpPosition(float newInterp)
        {
            if (newInterp < _distanceFromEdgeToClamp)
            {
                float strengthInterp = newInterp/_distanceFromEdgeToClamp;
                LumpStrength = strengthInterp;
            }else if (newInterp > 1 - _distanceFromEdgeToClamp)
            {
                float strengthInterp = (1 - newInterp)/_distanceFromEdgeToClamp;
                LumpStrength = strengthInterp;
            }
            else
            {
                LumpStrength = 1;
            }
            
            newInterp = Mathf.Clamp(newInterp, _distanceFromEdgeToClamp, 1 - _distanceFromEdgeToClamp);   
            _lumpPosition = newInterp;
        }
    }
}