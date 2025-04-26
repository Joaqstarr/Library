using DG.Tweening;
using UnityEngine;

namespace UI.Health
{
    public class HealthIcon : MonoBehaviour
    {
        
		 public void Show()
        {
            transform.DOKill();
            gameObject.SetActive(true);
            transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            transform.DOKill();
            transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

    }
}