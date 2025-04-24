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



        
        private bool _fired = false;
        
        [Serializable]
        private struct BindingDataContext
        {
            public string TrackName;
            public UnityEngine.Object BindingObject;
        }
        
        [SerializeField]
        private List<BindingDataContext> _bindings = new List<BindingDataContext>();
        
        private void Start()
        {

            StartCoroutine(WaitToCheckCutscene());
        }

        IEnumerator WaitToCheckCutscene()
        {
            yield return new WaitForSeconds(0.01f);
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



                if (Gamemanager.Instance)
                {
                    Gamemanager.Instance.GetSaveData().CutsceneFlags[_cutsceneToPlay.Cutscene.name] = true;
                    _fired = true;
                }
            }
        }
    }
}