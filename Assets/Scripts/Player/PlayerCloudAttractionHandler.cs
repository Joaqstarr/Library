using System;
using DG.Tweening;
using Systems.Interaction;
using UnityEngine;

namespace Player
{
    public class PlayerCloudAttractionHandler : MonoBehaviour
    {
        public enum AttractionStates
        {
            None,
            Attracting,
            FullyAttracted
        }
        
        protected AttractionStates _state = AttractionStates.None;
        
        private static readonly int AttractionPointId = Shader.PropertyToID("_AttractionPoint");
        private static readonly int AttractionStrengthId = Shader.PropertyToID("_AttractionStrength");
        private static readonly int AttractionFilterId = Shader.PropertyToID("_AttractionFilter");


        
        private PlayerInteractionManager _playerInteractionManager;
        
        private bool _attracting = false;
        private bool _fullyAttracting = false;
        
        [SerializeField]
        private Renderer[] _meshRenderers;


        private float _targetStrength = 0;
        private float _targetFilter = 0;

        [SerializeField]
        private float _lerpSpeed = 5;
        
        private Interactable _closestInteractable;
        private void Update()
        {
            
            SetCloudAttractionPoint();
            
            
            _state = AttractionStates.None;
            if (_attracting)
            {
                _state = AttractionStates.Attracting;
            }
            if (_fullyAttracting)
            {
                _state = AttractionStates.FullyAttracted;
            }
            
            switch (_state)
            {
                case AttractionStates.None:
                    _targetStrength = 0;
                    _targetFilter = 0;
                    break;
                case AttractionStates.Attracting:
                    _targetStrength = 0.5f;
                    _targetFilter = 0.1f;
                    break;
                case AttractionStates.FullyAttracted:
                    _targetStrength = 1f;
                    _targetFilter = 1f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            AttractionStrength = Mathf.Lerp(AttractionStrength, _targetStrength, Time.deltaTime * _lerpSpeed);
            AttractionFilter = Mathf.Lerp(AttractionFilter, _targetFilter, Time.deltaTime * _lerpSpeed);
        }

        private void SetCloudAttractionPoint()
        {
            if (_closestInteractable && _closestInteractable.IsCloudAttractor)
            {
                SetAttractionPoint(_closestInteractable.GetCloudAttractorPoint());
            }
        }

        private void Awake()
        {
            

            _playerInteractionManager = GetComponent<PlayerInteractionManager>();
        }

        private void OnEnable()
        {
            PlayerInteractionManager.OnClosestInteractableChanged += PlayerInteractionManagerOnOnClosestInteractableChanged;
        }
        
        private void OnDisable()
        {
            PlayerInteractionManager.OnClosestInteractableChanged -= PlayerInteractionManagerOnOnClosestInteractableChanged;
            
        }
        
        
        private void PlayerInteractionManagerOnOnClosestInteractableChanged(Interactable obj)
        {

            _closestInteractable = obj;
            if (!obj)
            {
                DisableLowAttraction();

                //disable attraction force
                return;
            }
            
            if (obj.IsCloudAttractor)
            {
                SetAttractionPoint(obj.GetCloudAttractorPoint());
            }
        }

        public void FullyAttract()
        {
            _fullyAttracting = true;
        }

        public void DisableFullAttraction()
        {
            _fullyAttracting = false;
        }
        public void DisableLowAttraction()
        {
            _attracting = false;
        }
        public void DisableAllAttraction()
        {
            _state = AttractionStates.None;
            _attracting = false;
            _fullyAttracting = false;
        }

        public void SetAttractionPoint(Vector3 pos)
        {
            AttractionPoint = pos;
            _attracting = true;
        }

        public Vector3 AttractionPoint
        {
            get => _meshRenderers[0].material.GetVector(AttractionPointId);
            set
            {
                foreach (var meshRenderer in _meshRenderers)
                {
                    Material _playerCloudMat = meshRenderer.material;
                    if (_playerCloudMat == null) continue;
                    
                    _playerCloudMat.SetVector(AttractionPointId, value);
                }
            }
        }

        public float AttractionStrength
        {
            get => _meshRenderers[0].material.GetFloat(AttractionStrengthId);
            set
            {
                foreach (var meshRenderer in _meshRenderers)
                {
                    Material _playerCloudMat = meshRenderer.material;
                    if (_playerCloudMat == null) continue;
                    
                    _playerCloudMat.SetFloat(AttractionStrengthId, value);
                }
            } 
        }

        public float AttractionFilter
        {
            get => _meshRenderers[0].material.GetFloat(AttractionFilterId);
            set
            {
                foreach (var meshRenderer in _meshRenderers)
                {
                    Material _playerCloudMat = meshRenderer.material;
                    if (_playerCloudMat == null) continue;

                    _playerCloudMat.SetFloat(AttractionFilterId, value);
                }
            }
        }
    }
}