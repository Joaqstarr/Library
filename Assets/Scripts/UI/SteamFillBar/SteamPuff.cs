using UnityEngine;
using UnityEngine.Serialization;

namespace UI.SteamFillBar
{
    public class SteamPuff : MonoBehaviour
    {
        [SerializeField] private float _speed = 100f; // Speed of movement
        private RectTransform _rectTransform;
        private RectTransform _parentRectTransform;

        [SerializeField] private float _maxSpeedRandom = 40;
        private float _speedModifier;
        
        private SteamFillBarStatus _statusBar;
        private void Awake()
        {
            _speedModifier = Random.Range(-_maxSpeedRandom, _maxSpeedRandom);
            _statusBar = transform.GetComponentInParent<SteamFillBarStatus>();
            _rectTransform = GetComponent<RectTransform>();
            _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        private void Update()
        {
            float speed = _speed;
            if (_statusBar != null)
            {
                speed = _statusBar.Speed;
            }
            
            speed += _speedModifier;
            
            // Move the UI element horizontally
            _rectTransform.anchoredPosition += Vector2.right * (speed * Time.deltaTime);

            // Get the parent's width
            float parentWidth = _parentRectTransform.rect.width;
            float elementWidth = _rectTransform.rect.width;

            // Check if the element is outside the parent's bounds
            if (_rectTransform.anchoredPosition.x > parentWidth / 2 + elementWidth / 2)
            {
                // Teleport to the left side
                _rectTransform.anchoredPosition = new Vector2(-parentWidth / 2 - elementWidth / 2, _rectTransform.anchoredPosition.y);
            }
            else if (_rectTransform.anchoredPosition.x < -parentWidth / 2 - elementWidth / 2)
            {
                // Teleport to the right side
                _rectTransform.anchoredPosition = new Vector2(parentWidth / 2 + elementWidth / 2, _rectTransform.anchoredPosition.y);
            }
        }
    }
}