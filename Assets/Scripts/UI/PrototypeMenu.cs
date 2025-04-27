using System;
using DG.Tweening;
using Systems.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PrototypeMenu : MonoBehaviour
    {
        [SerializeField] private string _levelToLoad = "CoreGamemode";
        [SerializeField] private Image _fadeImage;

        private void Start()
        {
            _fadeImage.DOFade(0, 1);

        }

        public void StartGame()
        {
            _fadeImage.DOFade(1, 1).onComplete += () =>
            {
                
                SceneManager.LoadScene(_levelToLoad);

            };
        }
        
        public void NewGame()
        {
            DataSaver saver = new DataSaver("save.boogers");
            saver.ClearData();
            StartGame();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}