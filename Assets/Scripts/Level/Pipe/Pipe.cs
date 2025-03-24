using System;
using Player;
using Systems.Interaction;
using UnityEngine;
using UnityEngine.Splines;

namespace Level.Pipe
{
    public class Pipe : MonoBehaviour
    {
        public Spline PipeSpline { get; private set; }
        [SerializeField]
        private PipeLump _pipeLump;

        [SerializeField] private PipeInteractable _pipeEntrancePrefab;
        private void Awake()
        {
            PipeSpline = GetComponent<SplineContainer>().Spline;
            
            PipeInteractable entrance1 = Instantiate(_pipeEntrancePrefab, transform);
            entrance1.transform.localPosition = PipeSpline.EvaluatePosition(0);
            entrance1.SetPipeInteractablePos(0);
            
            PipeInteractable entrance2 = Instantiate(_pipeEntrancePrefab, transform);
            entrance2.transform.localPosition = PipeSpline.EvaluatePosition(1);
            entrance2.SetPipeInteractablePos(1);

        }

        public void OnInteracted(PlayerStateManager player, float pos)
        {
            player.SwitchToPipeState(this, pos);
        }
        
        public void SetPipeLumpPosition(float newInterp)
        {
            _pipeLump.SetLumpPosition(newInterp);
        }

        public void EnablePipeLump()
        {
            _pipeLump.enabled = true;
        }

        public void DisablePipeLump()
        {
            _pipeLump.enabled = false;
        }
        
    }
}