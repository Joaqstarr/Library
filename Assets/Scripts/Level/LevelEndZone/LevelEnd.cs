using Player;
using Systems.CutsceneSystem;
using Systems.Gamemode;
using Systems.Interaction;
using UnityEngine;
using UnityEngine.Playables;

namespace Level.LevelEndZone
{
    public class LevelEnd : Interactable
    {
        [SerializeField]
        private int _levelIndex = 0;

        [SerializeField] private CutsceneData _cutsceneToPlay;
        protected override void InteractionTriggered(PlayerStateManager player)
        {
            if (Gamemanager.Instance)
            {
                if (Gamemanager.Instance.GetSaveData().IsLevelCompleted[_levelIndex])
                {
                    return;
                }
            }
            
            base.InteractionTriggered(player);

            if (Gamemanager.Instance)
            {
                Gamemanager.Instance.GetSaveData().IsLevelCompleted[_levelIndex] = true;
            }

            if (CutsceneEventManager.Instance && _cutsceneToPlay)
            {
                CutsceneEventManager.Instance.PlayCutscene(_cutsceneToPlay);
            }
            
            Destroy(gameObject);
        }
    }
}