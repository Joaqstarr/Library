using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PrototypeMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Archives");
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}