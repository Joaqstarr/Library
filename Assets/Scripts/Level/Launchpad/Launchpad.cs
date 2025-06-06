﻿using System;
using System.Collections;
using DG.Tweening;
using Player;
using Systems.Interaction;
using UnityEngine;

namespace Level.Launchpad
{
    public class Launchpad : Interactable
    {
        [SerializeField] private float _launchForce = 10f;
        [SerializeField] private int _trajectoryResolution = 30;

        public float LaunchForce
        {
            get => _launchForce;
        }

        public void SetIsInteractable(bool isInteractable)
        {
            IsInteractable = isInteractable;
        }

        protected override void InteractionTriggered(PlayerStateManager player)
        {
            base.InteractionTriggered(player);
            
            LaunchPlayer(player);
        }

        private void LaunchPlayer(PlayerStateManager player)
        {
            PlayerCloudAttractionHandler handler = player.GetComponent<PlayerCloudAttractionHandler>();

            handler.FullyAttract();
            DOVirtual.Vector3(player.transform.position, transform.position, 0.2f,(value) =>
            {
                player.PlayerMovementInstance.Teleport(value);
            }).onComplete += () =>
            {            
                handler.DisableFullAttraction();

                player.PlayerMovementInstance.LaunchCharacter(transform.forward * _launchForce);
            };
            

        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 startPosition = transform.position;
            Vector3 launchDirection = transform.forward * _launchForce;
            Vector3 previousPosition = startPosition;

            for (int i = 1; i <= _trajectoryResolution; i++)
            {
                float simulationTime = i / (float)_trajectoryResolution * 3;
                Vector3 displacement = launchDirection * simulationTime + Physics.gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = startPosition + displacement;
                Gizmos.DrawLine(previousPosition, drawPoint);
                previousPosition = drawPoint;
            }
        }
    }
}