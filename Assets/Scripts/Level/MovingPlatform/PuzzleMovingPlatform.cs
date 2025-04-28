using Unity.VisualScripting;
using UnityEngine;

namespace Level.MovingPlatform
{
    public class PuzzleMovingPlatform : MovingPlatformBehavior
    {
        [SerializeField]
        private float _resetTime = 5f;
        private float _resetTimer = 0f;

        private bool _isResetting = false;

        private bool _hasBegun = false;
        protected override void Update()
        {
            if (_isPlaying && !_isResetting)
            {
                _hasBegun = true;
            }
            if (_hasBegun && !_isPlaying)
            {
                _resetTimer -= Time.deltaTime;
                
                if(_resetTimer <= 0)
                {
                    _resetTimer = _resetTime;
                    _isPlaying = true;
                    _hasBegun = false;
                    _isResetting = true;
                }
            }
            else
            {
                _resetTimer = _resetTime;
            }

            if (_isResetting)
            {
                targetIndex = 0;
            }
            
            base.Update();


        }

        public override void StartMovement()
        {
            _isResetting = false;
            base.StartMovement();
        }
    }
}