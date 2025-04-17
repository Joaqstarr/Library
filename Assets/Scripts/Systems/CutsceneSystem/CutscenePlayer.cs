using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Systems.CutsceneSystem
{
    public class CutscenePlayer : MonoBehaviour
    {
        protected PlayableDirector _playableDirector;

        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();
        }

        private void OnEnable()
        {
            CutsceneEventManager.OnCutsceneStart += OnCutsceneStart;
        }

        private void OnDisable()
        {
            CutsceneEventManager.OnCutsceneStart -= OnCutsceneStart;

        }
        
        private void OnCutsceneStart(CutsceneData cutscenedata, ref List<BindingData> bindings)
        {
            if(cutscenedata.Cutscene == _playableDirector.playableAsset)
            {
                if (bindings != null)
                {
                    foreach (var binding in bindings)
                    {
                        foreach (var output in _playableDirector.playableAsset.outputs)
                        {
                            if (output.streamName == binding.TrackName) // your track name
                            {
                                _playableDirector.SetGenericBinding(output.sourceObject, binding.BindingObject);
                            }
                        }
                    }
                }
                
                
                _playableDirector.Play();
            }
        }
    }
}