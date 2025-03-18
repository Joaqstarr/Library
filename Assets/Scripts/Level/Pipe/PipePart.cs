using System;
using UnityEngine;

namespace Level.Pipe
{
    public class PipePart : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        private MeshFilter _meshFilter;
        private PipeLump _pipeLump;
        private void Start()
        {
            _pipeLump = transform.GetComponentInParent<PipeLump>();
            
            if(_meshRenderer)
                _meshFilter = _meshRenderer.gameObject.GetComponent<MeshFilter>();
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