using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScreenFader : MonoBehaviour
    {
        private Image _fadeImage;

        public static ScreenFader Instance;
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