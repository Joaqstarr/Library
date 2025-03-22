using System;
using Systems.Interaction;
using UnityEngine;

namespace Player
{
    public class PlayerCloudAttractionHandler : MonoBehaviour
    {
        private PlayerInteractionManager _playerInteractionManager;
        private Material _playerCloudMat;
        
        [SerializeField]
        private Renderer _meshRenderer;

        private void Awake()
        {
            
            _playerCloudMat = _meshRenderer.material;
            _playerInteractionManager = GetComponent<PlayerInteractionManager>();
        }

        private void OnEnable()
        {
            _playerInteractionManager.OnClosestInteractableChanged += PlayerInteractionManagerOnOnClosestInteractableChanged;
        }
        
        private void OnDisable()
        {
            _playerInteractionManager.OnClosestInteractableChanged -= PlayerInteractionManagerOnOnClosestInteractableChanged;
            
        }
        
        
        private void PlayerInteractionManagerOnOnClosestInteractableChanged(Interactable obj)
        {
            if (!obj)
            {
                //disable attraction force
                return;
            }
            
            if (obj.IsCloudAttractor)
            {
                _playerCloudMat.SetVector("_AttractionPoint", obj.GetCloudAttractorPoint());
            }
        }
    }
}