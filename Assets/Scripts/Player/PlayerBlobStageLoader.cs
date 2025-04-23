using System;
using Systems.Gamemode;
using Systems.SaveSystem;
using UnityEngine;

namespace Player
{
    public class PlayerBlobStageLoader : MonoBehaviour
    {
        [SerializeField] private GameObject[] _playerBlobStages;
        private void Awake()
        {
            if (Gamemanager.Instance)
            {
                int levelsComplete = Gamemanager.Instance.GetSaveData().GetLevelCompletedCount();
                
                for(int i = 0; i < _playerBlobStages.Length; i++)
                {
                    if (i == levelsComplete)
                    {
                        _playerBlobStages[i].SetActive(true);
                    }
                    else
                    {
                        _playerBlobStages[i].SetActive(false);
                    }
                }
                
            }
        }
    }
}