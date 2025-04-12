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

        private Vector3 _currentTargetPosition; // Current target position
        private bool _isStepping = false;

        private void Start()
        {
            // Initialize the target position to the current IK target position
            _currentTargetPosition = _ikTarget.position;
        }

        private void Update()
        {
            // Perform a raycast to find the ground position
            if (Physics.Raycast(_legOrigin.position, -_legOrigin.right, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                Vector3 desiredPosition = hit.point;

                // Check if the leg is far enough from the current target
                if (!_isStepping && Vector3.Distance(_ikTarget.position, desiredPosition) > _stepDistance)
                {
                    StartCoroutine(MoveToTarget(desiredPosition));
                }
            }
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
            _isStepping = false;
        }
    }
}