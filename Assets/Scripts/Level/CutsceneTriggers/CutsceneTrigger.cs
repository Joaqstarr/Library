using System;
using Systems.CutsceneSystem;
using UnityEngine;

namespace Level.CutsceneTriggers
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField]
        private CutsceneData _cutsceneToPlay;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (CutsceneEventManager.instance)
                {
                    CutsceneEventManager.instance.PlayCutscene(_cutsceneToPlay);
                }
            }
        }
    }
}