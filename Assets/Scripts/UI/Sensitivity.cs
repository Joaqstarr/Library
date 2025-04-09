using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class Sensitivity : MonoBehaviour
    {
        [SerializeField]
        private Slider _sensitivitySlider;
        [SerializeField]
        private InputActionAsset _inputAction;

        private InputAction _lookAction;
        public void SetSensitivity(float newSense)
        {
            FreelookSensSetter.Sens = newSense;

            PlayerPrefs.SetFloat("MouseSensitivity", newSense);
            PlayerPrefs.Save();
        }


        private void Start()
        {
            _lookAction = _inputAction.FindAction("Look");

            float sens = PlayerPrefs.GetFloat("MouseSensitivity", 1);
            FreelookSensSetter.Sens = sens;

            //Debug.Log(_lookAction.processors);
            _sensitivitySlider.value = sens;
        }
    }
}