using System;
using System.Collections;
using Player;
using Systems.CutsceneSystem;
using Systems.Gamemode;
using Systems.Interaction;
using UnityEngine;

namespace NPC.Grandma
{
    public class GrandmaWinScreen : Interactable
    {
        [SerializeField]
        private CutsceneData _cutsceneToPlay;
        
        private void Start()
        {

            StartCoroutine(WaitToCheckSave());
        }

        IEnumerator WaitToCheckSave()
        {
            yield return new WaitForSeconds(0.1f);
            if (Gamemanager.Instance)
            {
                int levelCompletedCount = Gamemanager.Instance.GetSaveData().GetLevelCompletedCount();

                if (levelCompletedCount < 3)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        
        protected override void InteractionTriggered(PlayerStateManager player)
        {
            base.InteractionTriggered(player);

            if (CutsceneEventManager.Instance)
            {
                CutsceneEventManager.Instance.PlayCutscene(_cutsceneToPlay);
            }
        }
    }
}