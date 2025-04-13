using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Animation
{
    public class AnimationSyncer : MonoBehaviour
    {
        private Animator _leaderAnimator;
        [SerializeField]
        private List<Animator> _followerAnimators;


        private void Awake()
        {
            if (_leaderAnimator == null)
            {
                _leaderAnimator = GetComponent<Animator>();
            }


        }

        void Update()
        {
            if (_leaderAnimator == null) return;

            SyncParameters();
            
            //SyncState();
        }

        void SyncParameters()
        {
            foreach (var follower in _followerAnimators)
            {
                if(!follower.enabled || !follower.gameObject.activeInHierarchy) continue;
                
                foreach (AnimatorControllerParameter param in _leaderAnimator.parameters)
                {
                    switch (param.type)
                    {
                        case AnimatorControllerParameterType.Bool:
                            follower.SetBool(param.name, _leaderAnimator.GetBool(param.name));
                            break;
                        case AnimatorControllerParameterType.Float:
                            follower.SetFloat(param.name, _leaderAnimator.GetFloat(param.name));
                            break;
                        case AnimatorControllerParameterType.Int:
                            follower.SetInteger(param.name, _leaderAnimator.GetInteger(param.name));
                            break;
                        case AnimatorControllerParameterType.Trigger:

                            break;
                    }
                }
            }
        }
        
        void SyncState()
        {
            AnimatorStateInfo leaderState = _leaderAnimator.GetCurrentAnimatorStateInfo(0);

            foreach (var follower in _followerAnimators)
            {
                follower.Play(leaderState.fullPathHash);
            }
        }

        public void SetTrigger(int triggerHash)
        {
            if(_leaderAnimator)
                _leaderAnimator.SetTrigger(triggerHash);
            
            foreach (var follower in _followerAnimators)
            {
                if(!follower.enabled || !follower.gameObject.activeInHierarchy) continue;

                follower.SetTrigger(triggerHash);
            }
        }
    }
}