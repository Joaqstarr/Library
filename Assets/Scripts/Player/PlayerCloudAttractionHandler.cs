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
        private Material _playerCloudMat;
        
        private bool _attracting = false;
        private bool _fullyAttracting = false;
        
        [SerializeField]
        private Renderer _meshRenderer;


        private float _targetStrength = 0;
        private float _targetFilter = 0;

        [SerializeField]
        private float _lerpSpeed = 5;
        private void Update()
        {
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

        private void Awake()
        {
            
            _playerCloudMat = _meshRenderer.material;
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

        protected Vector3 AttractionPoint
        {
            get => _playerCloudMat.GetVector(AttractionPointId);
            set => _playerCloudMat.SetVector(AttractionPointId, value);
        }

        protected float AttractionStrength
        {
            get => _playerCloudMat.GetFloat(AttractionStrengthId);
            set => _playerCloudMat.SetFloat(AttractionStrengthId, value);
        }

        protected float AttractionFilter
        {
            get => _playerCloudMat.GetFloat(AttractionFilterId);
            set => _playerCloudMat.SetFloat(AttractionFilterId, value);
        }
    }
}