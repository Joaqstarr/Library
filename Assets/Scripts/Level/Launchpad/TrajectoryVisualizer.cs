using UnityEngine;
using UnityEngine.VFX;

namespace Level.Launchpad
{
    public class TrajectoryVisualizer : MonoBehaviour
    {
        public VisualEffect vfxGraph;

        private Launchpad _launchpad;
        public int resolution = 50;
        public float timeStep = 0.05f;
        
        
        void OnEnable()
        {
            _launchpad = GetComponent<Launchpad>();

            Vector3 launchOrigin = transform.position;
            Vector3 initialVelocity = transform.forward * _launchpad.LaunchForce;
            Vector3[] points = new Vector3[resolution];
            Vector3 gravity = Physics.gravity;

            for (int i = 0; i < resolution; i++)
            {
                float t = i * timeStep;
                points[i] = launchOrigin + initialVelocity * t + 0.5f * gravity * t * t;
            }

            GraphicsBuffer buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, resolution, sizeof(float) * 3);
            buffer.SetData(points);
            vfxGraph.SetGraphicsBuffer("trajectoryBuffer", buffer);
            vfxGraph.SetInt("pointCount", resolution);
        }
    }

}