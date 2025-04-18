using System;
using DG.Tweening;
using UnityEngine;
using Utility.SceneManagement;

namespace Level.LevelSwitcher
{
    public class LevelSwitcherRotation : MonoBehaviour
    {
        [SerializeField] private Transform _levelSwitcherRotatorTransform;

        private LevelSwitcher _levelSwitcher;

        [SerializeField] private float _rotSpeed = 1;
        private void Awake()
        {
            _levelSwitcher = GetComponent<LevelSwitcher>();
        }
        

        private void OnEnable()
        {
            LevelSwitcher.OnLevelChanged += OnLevelChanged;
            
        }

        private void OnDisable()
        {
            LevelSwitcher.OnLevelChanged -= OnLevelChanged;
        }

        private void OnLevelChanged(int levelindex, SceneReference level)
        {
            float targetRotation = (360f / _levelSwitcher.GetLevelCount()) * _levelSwitcher.GetActiveLevelIndex();

            _levelSwitcherRotatorTransform.DOLocalRotate(new Vector3(0, targetRotation, 0), _rotSpeed).SetEase(Ease.OutBounce);
        }
    }
}