using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace NPC.Grandma
{
    public class GrandmaStateController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        [SerializeField]private Transform[] _points;
        private int _currentPointIndex = 0;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _currentPointIndex = Random.Range(0, _points.Length);
            
            transform.position = _points[_currentPointIndex].position;
            _currentPointIndex = Random.Range(0, _points.Length);

            
        }

        private void Start()
        {
            if (_points != null && _points.Length > 0)
            {
                StartCoroutine(WalkBetweenPoints());
            }
        }

        private IEnumerator WalkBetweenPoints()
        {
            while (true)
            {
                if (_points.Length == 0) yield break;

                _agent.SetDestination(_points[_currentPointIndex].position);

                while (_agent.pathPending || _agent.remainingDistance > _agent.stoppingDistance)
                {
                    yield return null;
                }

                float waitTime = Random.Range(1f, 5f);
                yield return new WaitForSeconds(waitTime);

                _currentPointIndex = Random.Range(0, _points.Length);

            }
        }

    }
}