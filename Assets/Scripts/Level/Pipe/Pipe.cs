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
        private void Awake()
        {
            PipeSpline = GetComponent<SplineContainer>().Spline;
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