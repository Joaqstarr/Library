using System;
using Systems.Gamemode;
using UnityEngine;
using UnityEngine.Playables;

namespace Player.Animation
{
    public class TransitionAnimationManager : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector[] _transitionAnimations;
        

        public void EnterNextTransition()
        {
            if (Gamemanager.Instance)
            {
                int levelsComplete = Gamemanager.Instance.GetSaveData().GetLevelCompletedCount();
                
                if (levelsComplete <= _transitionAnimations.Length)
                {
                    PlayableDirector playableDirector = _transitionAnimations[levelsComplete-1];
                    playableDirector.Play();
                }
            }
        }
    }
}