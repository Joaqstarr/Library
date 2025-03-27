using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        [field: SerializeField] public bool IsInteractable { get; protected set; } = true;

        [field: SerializeField]
        public bool IsCloudAttractor { get; protected set; } = true;

        [SerializeField] public Vector3 _cloudAttractionPoint;

        [SerializeField]
        protected UnityEvent OnInteractedEvent;
        public Vector3 GetCloudAttractorPoint()
        {
            return transform.TransformPoint(_cloudAttractionPoint);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(GetCloudAttractorPoint(), 0.1f);
        }

        public virtual void OnInteracted(PlayerStateManager player)
        {
            OnInteractedEvent?.Invoke();
        }
    }
}