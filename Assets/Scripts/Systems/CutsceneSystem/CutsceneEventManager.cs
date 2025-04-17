using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Systems.CutsceneSystem
{
    public class CutsceneEventManager : MonoBehaviour
    {

        public delegate void CutsceneEventDelegate(CutsceneData cutsceneData);
        public static CutsceneEventDelegate OnCutsceneStart;
        public static CutsceneEventManager instance;
        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }else
            {
                instance = this;
            }
        }

        public void PlayCutscene(CutsceneData cutsceneData)
        {
            if (cutsceneData == null)
            {
                Debug.LogError("Cutscene data is null");
                return;
            }

            OnCutsceneStart?.Invoke(cutsceneData);
        }

    }
}