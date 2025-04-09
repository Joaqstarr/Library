using UnityEngine;

namespace Utility
{
    public class AlwaysRenderSkinnedMesh : MonoBehaviour
    {
        private SkinnedMeshRenderer _skinnedMeshRenderer;

        void Start()
        {
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            if (_skinnedMeshRenderer != null)
            {
                // Set a large bounds to ensure the mesh is always rendered
                _skinnedMeshRenderer.bounds = new Bounds(transform.position, new Vector3(1000, 1000, 1000));
            }
        }
    }
}