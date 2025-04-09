using System.Collections;
using Level.Pipe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Player.States
{
    public class PipeState : BaseState
    {
        private Pipe _pipe;
        private Spline _spline;
        private float _splinePosition;
        private float _speed;

        private bool _finishedTransition = false;
        public PipeState(PlayerStateManager playerStateManager) : base(playerStateManager)
        {

        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            _playerStateManager.PlayerInteractionManagerInstance.enabled = false;
            _playerStateManager.PlayerAttackManagerInstance.enabled = false;

            _playerMovement.enabled = false;
            
            _playerStateManager.PlayerCloudAttractionHandlerInstance.FullyAttract();
            
            _speed = 5f;
            _finishedTransition = false;

            _playerStateManager.StartCoroutine(WaitToEnterPipe());
            
            IEnumerator WaitToEnterPipe()
            {
                yield return new WaitForSeconds(0.5f);
                _finishedTransition = true;
            }
            
            _pipe.EnablePipeLump();
        }

        protected override void OnExitState()
        {
            base.OnExitState();

            _playerStateManager.PlayerArt.forward = _playerStateManager.transform.forward;

            _playerStateManager.PlayerCloudAttractionHandlerInstance.AttractionPoint = _playerStateManager.transform.position;
            _pipe.DisablePipeLump();
            _playerStateManager.PlayerCloudAttractionHandlerInstance.DisableFullAttraction();
            _spline = null;
            _pipe = null;
            _playerMovement.enabled = true;
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();
 
            _pipe.SetPipeLumpPosition(_splinePosition);
            
            MoveAlongSpline();
        }

        private void MoveAlongSpline()
        {
            if(!_finishedTransition)return;

            // Get the player's directional input
            Vector2 input = _playerStateManager.PlayerControlsInstance.MoveInput;


            // Get the camera's forward vector
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0; // Ignore the vertical component
            cameraForward.Normalize();

            // Get the tangent direction at the current spline position
            Vector3 tangent = _spline.EvaluateTangent(_splinePosition);
            tangent.y = 0; // Ignore the vertical component
            tangent.Normalize();

            // Calculate the dot product to determine the movement direction
            Vector3 transformedTangent = _pipe.transform.TransformDirection(tangent);

            float direction = Vector3.Dot(cameraForward, transformedTangent) * input.y + Vector3.Dot(cameraForward, Vector3.Cross(Vector3.up, tangent)) * -input.x;

            if (direction < 0)
            {
                direction = -1;
            }else if (direction > 0)
            {
                direction = 1;
            }
            // Calculate the distance to move based on speed, time, and input direction
            float distance = _speed * Time.deltaTime * direction;

            // Update the spline position based on the distance
            SplineUtility.GetPointAtLinearDistance(_spline, _splinePosition, distance, out _splinePosition);

            if ((_splinePosition == 0 || _splinePosition == 1) && input.sqrMagnitude > 0)
            {
                _playerStateManager.SwitchToLocomotionState();
                return;
            }
            // Get the new position on the spline
            Vector3 newPosition = _spline.EvaluatePosition(_splinePosition);

            // Transform the position to the pipe's local space
            newPosition = _pipe.transform.TransformPoint(newPosition);

            // Update the player's position
            _playerStateManager.transform.position = newPosition;

            _playerStateManager.PlayerArt.forward = _playerStateManager.transform.position - Camera.main.transform.position;
        }

        public override void OnFixedUpdateState()
        {
            base.OnFixedUpdateState();
        }

        public void SetPipe(Pipe newPipe)
        {
            _pipe = newPipe;
            _spline = _pipe.PipeSpline;
        }

        public void SetPositionInPipe(float pos)
        {
            _splinePosition = pos;
        }
    }
}