using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Systems.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        [field: SerializeField] public bool IsInteractable { get; protected set; } = true;

        [field: SerializeField]
        public bool IsCloudAttractor { get; protected set; } = true;

        [SerializeField] public Vector3 _cloudAttractionPoint;


        public Vector3 GetCloudAttractorPoint()
        {
            return transform.TransformPoint(_cloudAttractionPoint);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(GetCloudAttractorPoint(), 0.1f);
        }
    }
}