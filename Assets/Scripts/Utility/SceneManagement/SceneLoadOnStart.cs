using System;
using UnityEngine;

namespace Utility.SceneManagement
{
    public class SceneLoadOnStart : MonoBehaviour
    {
        [SerializeField]
        private SceneReference _sceneReference;

        private void Start()
        {
            _sceneReference.LoadScene();
        }
    }
}