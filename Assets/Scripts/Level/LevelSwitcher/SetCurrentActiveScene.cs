using Systems.Gamemode;
using UnityEngine;
using Utility.SceneManagement;

namespace Level.LevelSwitcher
{
    public class SetCurrentActiveScene : MonoBehaviour
    {
        [SerializeField] private SceneReference _scene;

        public void SetActiveScene()
        {
            if (_scene != null)
            {
                if (Gamemanager.Instance)
                {
                    Gamemanager.Instance.SetCurrentLevel(_scene);
                }
            }

        }
    }
}