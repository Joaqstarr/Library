using System;
using System.Collections.Generic;
using Player;
using Systems.Gamemode;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Systems.CutsceneSystem
{

    public struct BindingData
    {
        public string TrackName;
        public UnityEngine.Object BindingObject;
        
        public BindingData(string trackName, UnityEngine.Object bindingObject)
        {
            TrackName = trackName;
            BindingObject = bindingObject;
        }
    }
    public class CutsceneEventManager : MonoBehaviour
    {

        public delegate void CutsceneEventDelegate(CutsceneData cutsceneData, ref List<BindingData> bindings);
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

        public void PlayCutscene(CutsceneData cutsceneData, List<BindingData> bindings = null)
        {
            if (cutsceneData == null)
            {
                Debug.LogError("Cutscene data is null");
                return;
            }

            if (bindings == null)
            {
                bindings = new List<BindingData>();
            }

            if (Gamemanager.Instance)
            {
                PlayerStateManager player = Gamemanager.Instance.GetPlayer();
                if (player)
                {
                    bindings.Add(new BindingData("Player", Gamemanager.Instance.GetPlayer().AnimatorInstance));
                    bindings.Add(new BindingData("PlayerSignal", Gamemanager.Instance.GetPlayer().PlayerSignalReceiverInstance));

                }
            }

            OnCutsceneStart?.Invoke(cutsceneData, ref bindings);
        }

    }
}