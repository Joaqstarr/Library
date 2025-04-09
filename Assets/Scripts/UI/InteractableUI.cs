using System;
using DG.Tweening;
using Player;
using Systems.Interaction;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InteractableUI : MonoBehaviour
    {
        private TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
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
            if (obj == null)
            {
                _text.text = "";
                return;
            }

            //transform.DOShakePosition(0.2f, 50, 5, 70);

            transform.DOPunchScale(Vector3.one * 0.3f, 0.15f, 6, 0f);
            _text.text = "Interact with " + obj.InteractableName;
        }
    }
}