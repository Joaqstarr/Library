using System;
using System.Collections.Generic;
using UnityEngine;
using Systems.Interaction;

namespace Player
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        public event Action<Interactable> OnClosestInteractableChanged;

        private List<Interactable> _interactablesInRange = new List<Interactable>();
        private Interactable _closestInteractable;
        private PlayerControls _playerControls;

        private void Awake()
        {
            _playerControls = GetComponentInParent<PlayerControls>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Interactable interactable = other.GetComponent<Interactable>();
            if (interactable != null && interactable.IsInteractable)
            {
                _interactablesInRange.Add(interactable);
                UpdateClosestInteractable();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Interactable interactable = other.GetComponent<Interactable>();
            if (interactable != null)
            {
                _interactablesInRange.Remove(interactable);
                UpdateClosestInteractable();
            }
        }

        private void Update()
        {
            UpdateClosestInteractable();
        }

        private void UpdateClosestInteractable()
        {
            Interactable closest = null;
            float closestDistance = float.MaxValue;

            foreach (var interactable in _interactablesInRange)
            {
                Vector3 directionToInteractable = interactable.transform.position - transform.position;
                float distance = directionToInteractable.magnitude;
               // float angle = Vector3.Angle(transform.forward, directionToInteractable);

                if (distance < closestDistance ) // Assuming a 45 degree field of view
                {
                    closest = interactable;
                    closestDistance = distance;
                }
            }

            if (closest != _closestInteractable)
            {
                _closestInteractable = closest;
                OnClosestInteractableChanged?.Invoke(_closestInteractable);
            }
        }

        public void InteractWithClosest()
        {
            if (_closestInteractable != null)
            {
                // Implement interaction logic here
                Debug.Log("Interacting with " + _closestInteractable.name);
            }
        }

        private void OnEnable()
        {
            _playerControls.OnInteractPressed += InteractWithClosest;
        }

        private void OnDisable()
        {
            _playerControls.OnInteractPressed -= InteractWithClosest;
        }
    }
}