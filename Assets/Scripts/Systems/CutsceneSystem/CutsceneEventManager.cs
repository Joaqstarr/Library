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
        public static CutsceneEventManager Instance;
        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
                return;
            }else
            {
                Instance = this;
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