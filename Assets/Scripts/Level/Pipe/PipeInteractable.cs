using System;
using Player;
using Systems.Interaction;
using UnityEngine;

namespace Level.Pipe
{
    public class PipeInteractable : Interactable
    {
        [SerializeField]
        private float _pipeInteractablePos = 1;
        private Pipe _pipe;
        private void Awake()
        {
            _pipe = GetComponentInParent<Pipe>();
        }

        public override void OnInteracted(PlayerStateManager player)
        {
            base.OnInteracted(player);
            
            _pipe.OnInteracted(player, _pipeInteractablePos);
        }
        
        public void SetPipeInteractablePos(float pos)
        {
            _pipeInteractablePos = pos;
        }
    }
}