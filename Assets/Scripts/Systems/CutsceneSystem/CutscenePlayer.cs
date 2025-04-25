using System;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
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
                    CinemachineVirtualCamera cutsceneCamObject = null; // Initialize to null

                    foreach (var binding in bindings)
                    {
                        foreach (var output in _playableDirector.playableAsset.outputs)
                        {
                            if (output.streamName == binding.TrackName) // your track name
                            {
                                _playableDirector.SetGenericBinding(output.sourceObject, binding.BindingObject);
                            }
                        }
                        
                        if (binding.TrackName == "CutsceneCam")
                        {
                            cutsceneCamObject = binding.BindingObject.GetComponent<CinemachineVirtualCamera>();
                        }
                    }


                    if (cutsceneCamObject != null) // Ensure it's assigned before using
                    {
                        foreach (var track in _playableDirector.playableAsset.outputs)
                        {
                            if (track.sourceObject is CinemachineTrack)
                            {
                                var cinemachineTrack = track.sourceObject as CinemachineTrack;
                                foreach( var clip in cinemachineTrack.GetClips() ){

                                    var cinemachineShot = clip.asset as CinemachineShot;
                                    _playableDirector.SetReferenceValue(cinemachineShot.VirtualCamera.exposedName, cutsceneCamObject);

                                }
                            }
                        }
                    }
                }

                
                _playableDirector.Play();
            }
        }
    }
}