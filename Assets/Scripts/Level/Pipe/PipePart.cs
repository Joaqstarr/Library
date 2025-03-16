using System;
using UnityEngine;

namespace Level.Pipe
{
    public class PipePart : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;
        private PipeLump _pipeLump;
        private void Start()
        {
            _pipeLump = transform.parent.GetComponent<PipeLump>();
        }

        private void Update()
        {
            if (_pipeLump && _meshRenderer)
            {
                _meshRenderer.material.SetVector("_LumpPosition", _pipeLump.LumpPosition);
            }
        }
    }
}