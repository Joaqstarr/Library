using System;
using System.Collections;
using DG.Tweening;
using Player;
using Systems.Gamemode;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScreenFader : MonoBehaviour
    {
        private Image _fadeImage;

        public static ScreenFader Instance;
        
        private bool _initialUnfade = false;
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                return;
            }
            
            _fadeImage = GetComponent<Image>();
            _fadeImage.DOFade(1, 0.01f);

        }

        private void Start()
        {
            if (!Gamemanager.Instance)
            {
                InitialUnfade();
            }
        }

        private void InitialUnfade()
        {
            if(_initialUnfade)return;
            _fadeImage.DOFade(0, 1f);
            _initialUnfade = true;
        }

        private void OnEnable()
        {
            Gamemanager.OnPlayerSpawned += (PlayerStateManager player) =>
            {
                InitialUnfade();
            };
        }


        public void Fade(float fadeDuration, float holdFadeDuration, Action onMiddle, Action onFadeComplete)
        {
            _fadeImage.DOFade(1, fadeDuration).onComplete += () =>
            {
                StartCoroutine(WaitAndUnfade(fadeDuration, holdFadeDuration, onMiddle, onFadeComplete));
            };
        }
        
        IEnumerator WaitAndUnfade(float fadeDuration, float holdFadeDuration, Action onMiddle, Action onFadeComplete)
        {
            onMiddle?.Invoke();

            yield return new WaitForSeconds(holdFadeDuration);

            _fadeImage.DOFade(0, fadeDuration).onComplete += () =>
            {
                onFadeComplete?.Invoke();
            };
        }
    }
}