using System;
using Systems.CutsceneSystem;
using Systems.Gamemode;
using UnityEngine;

namespace Level.CutsceneTriggers
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField]
        private CutsceneData _cutsceneToPlay;


        private bool _fired = false;
        private void Start()
        {
            //check save data
            if (Gamemanager.Instance)
            {
                Gamemanager.Instance.GetSaveData().CutsceneFlags.TryGetValue(_cutsceneToPlay.Cutscene.name, out bool cutscenePlayed);
                _fired = cutscenePlayed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_fired && other.CompareTag("Player"))
            {
                if (CutsceneEventManager.Instance)
                {
                    CutsceneEventManager.Instance.PlayCutscene(_cutsceneToPlay);
                    
                }

                if (Gamemanager.Instance)
                {
                    Gamemanager.Instance.GetSaveData().CutsceneFlags[_cutsceneToPlay.Cutscene.name] = true;
                    _fired = true;
                    Gamemanager.Instance.SaveData();
                }
            }
        }
    }
}