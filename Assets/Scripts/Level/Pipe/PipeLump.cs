using System;
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
        
        private void Awake()
        {
            _splineContainer = GetComponent<SplineContainer>();
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
    }
}