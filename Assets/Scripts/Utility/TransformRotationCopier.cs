using System;
using UnityEngine;

namespace Utility
{
    public class TransformRotationCopier : MonoBehaviour
    {
        [SerializeField]private Transform _transformToCopy;

        private void Update()
        {
            transform.localRotation = _transformToCopy.localRotation;
        }
    }
}