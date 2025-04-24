using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class SwitchToWinLevel : MonoBehaviour
    {
        [SerializeField]
        private string _winLevelName = "WinLevel";

        public void LoadWinLevel()
        {
            if (ScreenFader.Instance)
            {
                ScreenFader.Instance.Fade(3, 1, () =>
                {
                    SceneManager.LoadScene(_winLevelName);
                }, null);
            }
            else
            {
                SceneManager.LoadScene(_winLevelName);
            }
        }
    }
}