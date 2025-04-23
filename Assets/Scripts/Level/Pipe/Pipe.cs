using System;
using Player;
using Systems.Interaction;
using Unity.Mathematics;
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
        
        [SerializeField]
        private bool _autoSpawnPipeEntrances = true;
        private void Awake()
        {
            PipeSpline = GetComponent<SplineContainer>().Spline;

            if (_autoSpawnPipeEntrances)
            {
                SpawnPipeEntrance(0);
                SpawnPipeEntrance(1);
            }
        }

        private void SpawnPipeEntrance(float interp)
        {
            PipeInteractable entrance = Instantiate(_pipeEntrancePrefab, transform);

            float3 pos;
            float3 tan;
            float3 up;
            PipeSpline.Evaluate(interp, out pos, out tan, out up);
            
            float dif = (interp > 0.5f) ? -0.4f : 0.4f;
            float result;
            PipeSpline.GetPointAtLinearDistance(interp, dif, out result);
            
            float3 prePos = PipeSpline.EvaluatePosition(result);

            Vector3 dir = ((Vector3)pos - (Vector3)prePos).normalized;

            entrance.transform.localPosition = pos;
            entrance.transform.forward = transform.TransformDirection(dir);
            entrance.SetPipeInteractablePos(interp);
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