using System;
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
        
        private void OnCutsceneStart(CutsceneData cutscenedata)
        {
            if (cutscenedata.Cutscene == _playableDirector.playableAsset)
            {
                _playableDirector.Play();
            }
        }
    }
}