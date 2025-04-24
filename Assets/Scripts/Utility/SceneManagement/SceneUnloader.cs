using UnityEngine;

namespace Utility.SceneManagement
{
    public class SceneUnloader : MonoBehaviour
    {
        [SerializeField] private SceneReference _scene;
        public void UnloadScene()
        {
            _scene.UnloadScene();
        }
    }
}