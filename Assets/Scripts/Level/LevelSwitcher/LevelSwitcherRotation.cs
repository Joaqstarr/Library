using System;
using System.Collections;
using DG.Tweening;
using Systems.Steam;
using UnityEngine;
using Utility.SceneManagement;

namespace Level.LevelSwitcher
{
    public class LevelSwitcherRotation : MonoBehaviour
    {
        [SerializeField] private Transform _levelSwitcherRotatorTransform;

        private LevelSwitcher _levelSwitcher;

        [SerializeField] private float _rotSpeed = 1;
        [SerializeField] private float _rotPunchTime = 1;
        [SerializeField] private float _rotPunch = 10;
        [SerializeField] private SteamResourceHolder _steamResourceHolder;
        [SerializeField] private AudioSource _grindAudioSource;
        private float _currentRotation;
        private float _targetRotation;
        
        private float _oldFillPercent = 0;

        private void Awake()
        {
            _currentRotation = _levelSwitcherRotatorTransform.localEulerAngles.y;
            if (_currentRotation > 180)
            {
                _currentRotation -= 360;
            }
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
        private void Update()
        {
            float targVolume = 0;
            if (_steamResourceHolder.enabled)
            {
                _targetRotation = (360f / _levelSwitcher.GetLevelCount()) * (_levelSwitcher.GetActiveLevelIndex() + 1);

                float newPercent = _steamResourceHolder.SteamFillPercent;
                
                
                _levelSwitcherRotatorTransform.localEulerAngles = new Vector3(0,
                    Mathf.Lerp(_currentRotation, _targetRotation, newPercent),
                    0);
                
                
                if (_grindAudioSource)
                {
                    if (_steamResourceHolder.enabled)
                    {
                        float delta = (newPercent - _oldFillPercent) / Time.deltaTime;
                        
                        Debug.Log(delta);
                        if (Mathf.Abs(delta) > 0.1f)
                        {
                            targVolume = 1;
                        }
                        else
                        {
                            targVolume = 0;
                        }
                    }
                }
                _oldFillPercent = _steamResourceHolder.SteamFillPercent;
            }
            

            
            if (_grindAudioSource)
            {
                _grindAudioSource.volume = Mathf.Lerp(_grindAudioSource.volume, targVolume, Time.deltaTime * 5);
            }
        }

        private void OnLevelChanged(int levelindex, SceneReference level)
        {
            _steamResourceHolder.enabled = false;
            float targetRotation = (360f / _levelSwitcher.GetLevelCount()) * _levelSwitcher.GetActiveLevelIndex();
            _currentRotation = targetRotation;
            _levelSwitcherRotatorTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), _rotPunchTime);

            _levelSwitcherRotatorTransform.DOPunchRotation(new Vector3(0, _rotPunch, 0), _rotPunchTime);
            StartCoroutine(WaitToFinish());
            IEnumerator WaitToFinish()
            {
                yield return new WaitForSeconds(_rotSpeed);
                _steamResourceHolder.SetSteamAmount(0);
                _steamResourceHolder.enabled = true;
            }
        }
    }
}