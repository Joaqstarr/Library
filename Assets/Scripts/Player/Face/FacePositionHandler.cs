using System.Linq;
using UnityEngine;

namespace Player.Face
{
    public class FacePositionHandler : MonoBehaviour
    {
        [SerializeField] private Transform[] _facePositions;
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _rotationSpeed = 2f;

        private void Update()
        {
            int activeCount = 0;

            // Calculate average position
            Vector3 averagePosition = Vector3.zero;
            Quaternion averageRotation = Quaternion.identity;

            foreach (var t in _facePositions)
            {
                if(t.gameObject.activeInHierarchy)
                {
                    activeCount++;
                    averagePosition += t.position;

                }
            }
            averagePosition /= activeCount;

            // Calculate average rotation
            foreach (var t in _facePositions)
            {
                if (t.gameObject.activeInHierarchy)
                {
                    averageRotation = Quaternion.Slerp(averageRotation, t.rotation, 1f / activeCount);
                }
            }

            // Move towards the average position
            transform.position = Vector3.Lerp(transform.position, averagePosition, Time.deltaTime * _moveSpeed);

            // Rotate towards the average rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, averageRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}