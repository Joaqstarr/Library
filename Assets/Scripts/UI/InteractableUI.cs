using System;
using System.Linq;
using DG.Tweening;
using Player;
using Systems.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace UI
{
    public class InteractableUI : MonoBehaviour
    {
        private TMP_Text _text;

        private InputDevice _currentDevice;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

        }
        
        private void OnAnyButtonPress(InputControl control)
        {
            // Handle the input event here
            _currentDevice = control.device;

        }

        private void OnEnable()
        {
            InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
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

            //InputSystem.GetDevice()
            //transform.DOShakePosition(0.2f, 50, 5, 70);

            transform.DOPunchScale(Vector3.one * 0.3f, 0.15f, 6, 0f);

            string controlText;


            if (_currentDevice is Gamepad)
            {
                controlText = "<font=\"promptfont SDF\">\u21a4</font>";
            }
            else if (_currentDevice is Keyboard || _currentDevice is Mouse)
            {
                controlText = "E";
            }
            else
            {
                controlText = "?"; // Fallback for unknown devices
            }
            _text.text = controlText + " to " + obj.InteractVerb + " " + obj.InteractableName;
            
            
        }
    }
}