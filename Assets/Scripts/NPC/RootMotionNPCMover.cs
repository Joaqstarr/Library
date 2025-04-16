using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class RootMotionNPCMover : MonoBehaviour
    {
        private static readonly int ShouldMoveParameter = Animator.StringToHash("ShouldMove");
        private static readonly int SpeedParameter = Animator.StringToHash("Speed");

        private Animator _animator;
        private NavMeshAgent _agent;

        private Vector2 _smoothDeltaPosition;
        private Vector2 _velocity;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.updatePosition = false;
            _agent.updateRotation = true;
        }

        private void Update()
        {
            if (!_agent.isActiveAndEnabled || !_agent.isOnNavMesh) return;

            Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;
            worldDeltaPosition.y = 0;

            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);

            Vector3 deltaPosition = new Vector3(dx, dy);

            float smooth = Mathf.Min(1, Time.deltaTime / 0.15f);

            _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

            _velocity = _smoothDeltaPosition / Time.deltaTime;
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _velocity = Vector2.Lerp(
                    Vector2.zero,
                    _velocity,
                    _agent.remainingDistance / _agent.stoppingDistance
                );
            }

            bool shouldMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.stoppingDistance;

            _animator.SetBool(ShouldMoveParameter, shouldMove);
            _animator.SetFloat(SpeedParameter, _velocity.magnitude);
        }

        private void OnAnimatorMove()
        {
            Vector3 rootPosition = _animator.rootPosition;
            rootPosition.y = _agent.nextPosition.y;

            // Constrain the position to the NavMesh
            if (NavMesh.SamplePosition(rootPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                rootPosition = hit.position;
            }

            transform.position = rootPosition;
            _agent.nextPosition = rootPosition;
        }
    }
}