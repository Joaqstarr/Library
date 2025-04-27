using Audio;
using Cinemachine;
using UnityEngine;

namespace Level.PlatformRobot
{
    public class IKLeg : MonoBehaviour
    {
        [SerializeField]
        private Transform _ikTarget; // The IK target to move
        [SerializeField]
        private Transform _legOrigin; // The origin point of the leg
        [SerializeField]
        private float _stepDistance = 1.0f; // Distance threshold to trigger a step
        [SerializeField]
        private float _stepHeight = 0.5f; // Height of the step
        [SerializeField]
        private float _stepSpeed = 5.0f; // Speed of the step
        [SerializeField]
        private LayerMask _groundLayer; // Layer mask for ground detection

        [SerializeField] private RandomClipPlayer _randomClipPlayer;
        private Vector3 _currentTargetPosition; // Current target position
        private bool _isStepping = false;

        public bool IsMoving {get{ return _isStepping; } }
        
        [SerializeField]
        private IKLeg[] _linkedLegs; // Linked legs for synchronization

        [SerializeField] private float _postStepWait = 0.5f;

        private CinemachineImpulseSource _cinemachineImpulse;
        private void Start()
        {
            _cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
            // Initialize the target position to the current IK target position
            _currentTargetPosition = _ikTarget.position;
        }

        private void Update()
        {
            Debug.DrawRay(_legOrigin.position, -_legOrigin.right * 10, Color.red);
            // Perform a raycast to find the ground position
            if (Physics.Raycast(_legOrigin.position + _legOrigin.parent.forward * 15, -_legOrigin.right, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                Vector3 desiredPosition = hit.point;


                // Check if the leg is far enough from the current target
                if (!_isStepping && !IsOtherLegMoving() && Vector3.Distance(_ikTarget.position, desiredPosition) > _stepDistance)
                {
                    StartCoroutine(MoveToTarget(desiredPosition));
                }
            }
        }

        private bool IsOtherLegMoving()
        {
            if(_linkedLegs.Length > 0)
            {
                foreach (IKLeg leg in _linkedLegs)
                {
                    if(leg.IsMoving) return true;
                }
            }

            return false;
        }
        private System.Collections.IEnumerator MoveToTarget(Vector3 newTargetPosition)
        {
            _isStepping = true;

            Vector3 startPosition = _ikTarget.position;
            float stepProgress = 0.0f;

            while (stepProgress < 1.0f)
            {
                stepProgress += Time.deltaTime * _stepSpeed;

                // Interpolate position and add a vertical arc for the step
                Vector3 interpolatedPosition = Vector3.Lerp(startPosition, newTargetPosition, stepProgress);
                
                Vector3 dirToLegOrigin = (_legOrigin.position - _ikTarget.position).normalized;
                interpolatedPosition += dirToLegOrigin * Mathf.Sin(stepProgress * Mathf.PI) * _stepHeight;

                _ikTarget.position = interpolatedPosition;
                yield return null;
            }

            _ikTarget.position = newTargetPosition;
            _currentTargetPosition = newTargetPosition;
            if (_randomClipPlayer)
            {
                _randomClipPlayer.PlayRanClip();
            }
            if(_cinemachineImpulse)
                _cinemachineImpulse.GenerateImpulseAtPositionWithVelocity(_ikTarget.position, _cinemachineImpulse.m_DefaultVelocity);
            yield return new WaitForSeconds(_postStepWait);
            _isStepping = false;
        }
    }
}