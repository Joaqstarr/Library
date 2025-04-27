using System;
using System.Collections;
using System.Collections.Generic;
using Systems.CutsceneSystem;
using Systems.Gamemode;
using UnityEngine;

namespace Level.CutsceneTriggers
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField]
        private CutsceneData _cutsceneToPlay;



        
        private bool _fired = true;
        [SerializeField]
        private bool _shouldSaveData = true;
        
        [SerializeField]
        private List<BindingData> _bindings = new List<BindingData>();
        
        private void Start()
        {

            StartCoroutine(WaitToCheckCutscene());
        }

        IEnumerator WaitToCheckCutscene()
        {
            yield return new WaitForSeconds(0.01f);
            //check save data
            if (_shouldSaveData && Gamemanager.Instance)
            {
                Gamemanager.Instance.GetSaveData().CutsceneFlags.TryGetValue(_cutsceneToPlay.Cutscene.name, out bool cutscenePlayed);
                _fired = cutscenePlayed;
            }

            if (!_shouldSaveData)
            {
                _fired = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerCutscene();
            }
        }

        public void TriggerCutscene()
        {
            if (_fired) return;
            if (CutsceneEventManager.Instance)
            {
                List<BindingData> bindings = new List<BindingData>();

                if (_bindings != null)
                {
                    foreach (var bindingDataContext in _bindings)
                    {
                        bindings.Add(new BindingData(bindingDataContext.TrackName, bindingDataContext.BindingObject));
                    }
                }
                CutsceneEventManager.Instance.PlayCutscene(_cutsceneToPlay, bindings);
                    
            }


            if (_shouldSaveData && Gamemanager.Instance)
            {
                Gamemanager.Instance.GetSaveData().CutsceneFlags[_cutsceneToPlay.Cutscene.name] = true;
                _fired = true;
            }
        }
    }
}