using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PrototypeMenu : MonoBehaviour
    {
        [SerializeField] private string _levelToLoad = "CoreGamemode";
        public void StartGame()
        {
            SceneManager.LoadScene(_levelToLoad);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}