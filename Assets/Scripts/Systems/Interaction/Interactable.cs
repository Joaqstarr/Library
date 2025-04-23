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

        [SerializeField] private float _debounceTime = 0.5f;
        private float _debounceTimer;
        [field: SerializeField]
        public bool IsCloudAttractor { get; protected set; } = true;

        [SerializeField] public Vector3 _cloudAttractionPoint;

        [SerializeField]
        protected UnityEvent OnInteractedEvent;
        [SerializeField]
        protected UnityEvent<PlayerStateManager> OnInteractedEventWithPlayer;
        [SerializeField]
        public string InteractableName = "Interactable";
        public Vector3 GetCloudAttractorPoint()
        {
            return transform.TransformPoint(_cloudAttractionPoint);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(GetCloudAttractorPoint(), 0.1f);
        }

        public void OnInteracted(PlayerStateManager player)
        {
            if(_debounceTimer > 0)return;
            _debounceTimer = _debounceTime;

            InteractionTriggered(player);

        }

        protected virtual void InteractionTriggered(PlayerStateManager player)
        {
            OnInteractedEvent?.Invoke();
            OnInteractedEventWithPlayer?.Invoke(player);
        }

        private void Update()
        {
            _debounceTimer -= Time.deltaTime;
        }
    }
}