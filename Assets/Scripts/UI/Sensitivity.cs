using System;
using Player;
using UnityEngine;

namespace UI
{
    public class Sensitivity : MonoBehaviour
    {
        public void SetSensitivity(float newSense)
        {
            PlayerControls.LookSensitivity = newSense;
            PlayerPrefs.SetFloat("MouseSensitivity", newSense);
            PlayerPrefs.Save();
        }

        private void Start()
        {
            PlayerControls.LookSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1);
        }
    }
}