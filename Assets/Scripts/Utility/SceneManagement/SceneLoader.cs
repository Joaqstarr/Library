using System;
using UnityEngine;

namespace Utility.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneReference _sceneToLoad;

        private void Start()
        {
            _sceneToLoad.LoadScene();
            
            //Uncomment this line to unload the scene after 5 seconds
            Invoke(nameof(UnloadScene), 5);
            
            
        }

        void UnloadScene()
        {
            _sceneToLoad.UnloadScene();
        }
    }
}